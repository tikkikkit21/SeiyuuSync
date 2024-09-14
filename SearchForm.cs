using SeiyuuSync.JsonClasses;
using SeiyuuSync.Utils;
using System.Text.Json;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;
using Amazon.Runtime.Internal.Transform;

namespace SeiyuuSync
{
    public partial class SearchForm : Form
    {



        public SearchForm()
        {
            InitializeComponent();
        }

        private async Task<Dictionary<string, List<Character>>> FindCommonVas(string animeName, Dictionary<string, List<string>> vaDict)
        {
            Dictionary<string, List<Character>> commonVas = new Dictionary<string, List<Character>>();
            DbController dbController = new DbController();
            foreach (KeyValuePair<string, List<string>> kvp in vaDict)
            {
                string vaName = kvp.Key;
                VoiceActor va = await dbController.FindVoiceActor(vaName);

                if (va != null && va.Characters.Any(c => c.AnimeName != animeName))
                {
                    commonVas.Add(vaName, va.Characters.Where(c => c.AnimeName != animeName).ToList());
                }
            }

            return commonVas;
        }

        private async void AddVoiceActors(string animeName, Dictionary<string, List<string>> vaDict)
        {
            DbController dbController = new DbController();
            foreach (KeyValuePair<string, List<string>> kvp in vaDict)
            {
                string vaName = kvp.Key;
                List<string> characters = kvp.Value;

                if (await dbController.FindVoiceActor(vaName) == null)
                {
                    List<Character> list = characters.Select(c => new Character { AnimeName = animeName, CharacterName = c}).ToList();
                    VoiceActor actor = new VoiceActor
                    {
                        Name = vaName,
                        Characters = list
                    };
                    await dbController.AddVoiceActor(actor);
                }
            }
        }

        private async Task<Dictionary<string, List<string>>> FindVoiceActors(string selectedAnime)
        {
            ApiController controller = new ApiController();

            // va, [char]
            Dictionary<string, List<string>> vaDict = await controller.FindVoiceActors(selectedAnime);
            //AddVoiceActors(selectedAnime, vaDict);
            return vaDict;
        }

        private async void AddButton_Click(object sender, EventArgs e)
        {
            ApiController controller = new ApiController();
            int animeId = (int)dgvAnimeList.SelectedCells[colAnimeId.Index].Value;
            await controller.AddAnime(animeId);

            // add to database
            string selectedAnime = (string)dgvAnimeList.SelectedCells[colAnimeName.Index].Value;
            AddVoiceActors(selectedAnime, await controller.FindVoiceActors(selectedAnime));
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string animeName = tbxSearch.Text;
            ApiController controller = new ApiController();
            AnimeSearchResponse response = await controller.FindAnime(animeName);

            dgvAnimeList.Rows.Clear();
            dgvAnimeList.ClearSelection();
            foreach (Node node in response.Nodes)
            {
                dgvAnimeList.Rows.Add(node.Anime.Id, node.Anime.Title);
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
