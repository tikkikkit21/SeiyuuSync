using SeiyuuSync.JsonClasses;
using SeiyuuSync.Utils;
using System.Text.Json;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;
using Amazon.Runtime.Internal.Transform;
using System.Xml.Linq;

namespace SeiyuuSync
{
    public partial class SearchForm : Form
    {

        bool split = false;
        private static ApiController apiController;
        private static DbController dbController;


        public SearchForm()
        {
            InitializeComponent();
            apiController = new ApiController();
            dbController = new DbController();
        }

        /// <summary>
        /// Finds all common VAs. This method takes in a List of VAs and compares it with all VAs in the database
        /// </summary>
        /// <param name="animeName">Name of anime</param>
        /// <param name="vaList">Dictionary of all VAs for this anime</param>
        /// <returns>Dictionary of common VAs</returns>
        private async Task<Dictionary<string, List<Character>>> FindCommonVas(string animeName, List<VoiceActor> vaList)
        {
            Dictionary<string, List<Character>> commonVas = new Dictionary<string, List<Character>>();
            foreach (VoiceActor voiceActor in vaList)
            {
                VoiceActor va_db = await dbController.FindVoiceActor(voiceActor.Name);
                if (va_db.Characters.Any(c => c.AnimeName != animeName))
                {
                    commonVas.Add(va_db.Name, va_db.Characters.Where(c => c.AnimeName != animeName).ToList());
                }
            }

            return commonVas;
        }

        /// <summary>
        /// Adds all VAs in dictionary to MAL and DB
        /// </summary>
        /// <param name="animeName">Name of anime</param>
        /// <param name="vaList">Dictionary of VA info</param>
        private async void AddVoiceActors(string animeName, List<VoiceActor> vaList)
        {
            try
            {
                foreach (VoiceActor voiceActor in vaList)
                {
                    string vaName = voiceActor.Name;
                    List<Character> characters = voiceActor.Characters;

                    if (await dbController.FindVoiceActor(vaName) == null)
                    {
                        VoiceActor actor = new VoiceActor
                        {
                            Name = vaName,
                            Characters = characters
                        };
                        await dbController.AddVoiceActor(actor);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Finds all voice actors for a specificed anime
        /// </summary>
        /// <param name="selectedAnime">Name of anime</param>
        /// <returns>Dictionary of VA info</returns>
        private async Task<List<VoiceActor>> FindVoiceActors(string selectedAnime)
        {
            List<VoiceActor> vaList = new List<VoiceActor>();
            try
            {
                vaList = await apiController.FindVoiceActors(selectedAnime);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return vaList;
        }

        private async void AddButton_Click(object sender, EventArgs e)
        {
            int animeId = (int)dgvAnimeList.SelectedCells[colAnimeId.Index].Value;
            await apiController.AddAnime(animeId);

            // add to database
            string selectedAnime = (string)dgvAnimeList.SelectedCells[colAnimeName.Index].Value;
            AddVoiceActors(selectedAnime, await apiController.FindVoiceActors(selectedAnime));
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string animeName = tbxSearch.Text;
            List<Anime> animes= await apiController.FindAnime(animeName);

            dgvAnimeList.Rows.Clear();
            dgvAnimeList.ClearSelection();
            foreach (Anime anime in animes)
            {
                dgvAnimeList.Rows.Add(anime.Id, anime.Title);
            }
        }

        private async void CompareButton_Click(object sender, EventArgs e)
        {
            string selectedAnime = (string)dgvAnimeList.SelectedCells[colAnimeName.Index].Value;
            Dictionary<string, List<Character>> voiceActors = await FindCommonVas(selectedAnime, await FindVoiceActors(selectedAnime));


            //tableLayoutPanel1.Controls.Remove(dgvAnimeList);
            //tableLayoutPanel1.Controls.Add(dgvAnimeList, 1, 1); // Move button to cell (2,2)
            //tableLayoutPanel1.SetColumnSpan(dgvAnimeList, 10);



            //FlowLayoutPanel voiceActorFlow = new FlowLayoutPanel
            //{
            //    Dock = DockStyle.Fill,
            //    AutoScroll = true, // Enable automatic scrolling
            //    FlowDirection = FlowDirection.TopDown, // Stack panels vertically
            //    WrapContents = false // Prevent wrapping, keep adding vertically
            //};

            //tableLayoutPanel1.Controls.Add(voiceActorFlow, 11, 1);
            //tableLayoutPanel1.SetColumnSpan(voiceActorFlow, 8);

            voiceActorFlow.Controls.Clear();

            foreach (var actor in voiceActors)
            {

                Panel panel = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Width = voiceActorFlow.Width - 50,
                    Height = 500,
                    Margin = new Padding(10)
                };

                // Create and configure TableLayoutPanel
                TableLayoutPanel tableLayout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 2,
                    ColumnStyles =
                    {
                        new ColumnStyle(SizeType.Percent, 50F),
                        new ColumnStyle(SizeType.Percent, 50F)
                    },
                        RowStyles =
                    {
                        new RowStyle(SizeType.Percent, 50F),
                        new RowStyle(SizeType.Percent, 50F)
                    }
                };

                // Create and add PictureBox to top-left cell
                PictureBox pictureBox = new PictureBox
                {
                    //Image = Image.FromFile("path_to_picture.jpg"), // Replace with actual image path
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Dock = DockStyle.Fill
                };
                tableLayout.Controls.Add(pictureBox, 0, 0);

                // Create and add Label to top-right cell
                Label nameLabel = new Label
                {
                    Text = actor.Key,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                tableLayout.Controls.Add(nameLabel, 1, 0);

                // Create and add inner FlowLayoutPanel to bottom cells
                FlowLayoutPanel movieFlowLayout = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = true
                };

                foreach (var character in actor.Value)
                {
                    Label movieLabel = new Label
                    {
                        Text = character.AnimeName,
                        Margin = new Padding(5),
                        AutoSize = true
                    };
                    movieFlowLayout.Controls.Add(movieLabel);
                }

                // Add FlowLayoutPanel to bottom-left cell
                tableLayout.Controls.Add(movieFlowLayout, 0, 1);

                // Add empty panel to bottom-right cell (if needed, can be omitted)
                Panel emptyPanel = new Panel
                {
                    Dock = DockStyle.Fill
                };
                tableLayout.Controls.Add(emptyPanel, 1, 1);

                // Add TableLayoutPanel to the main Panel
                panel.Controls.Add(tableLayout);




                // Add the Panel to the FlowLayoutPanel
                voiceActorFlow.Controls.Add(panel);


            }
        }
    }
}
