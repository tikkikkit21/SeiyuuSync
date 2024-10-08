﻿using SeiyuuSync.JsonClasses;
using SeiyuuSync.Utils;

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
        private async Task<List<VoiceActor>> FindCommonVas(string animeName, List<VoiceActor> vaList)
        {
            List<VoiceActor> commonVas = new List<VoiceActor>();
            foreach (VoiceActor voiceActor in vaList)
            {
                VoiceActor va_db = await dbController.FindVoiceActor(voiceActor.Name);
                if (va_db!= null && va_db.Characters.Any(c => c.AnimeName != animeName))
                {
                    commonVas.Add(va_db);
                }
            }

            return commonVas;
        }

        /// <summary>
        /// Adds all VAs in dictionary to MAL and DB
        /// </summary>
        /// <param name="animeName">Name of anime</param>
        /// <param name="vaList">Dictionary of VA info</param>
        private async Task AddVoiceActors(string animeName, List<VoiceActor> vaList)
        {
            try
            {
                foreach (VoiceActor voiceActor in vaList)
                {
                    string vaName = voiceActor.Name;
                    List<Character> characters = voiceActor.Characters;

                    if (await dbController.FindVoiceActor(vaName) == null)
                    {
                        await dbController.AddVoiceActor(voiceActor);
                    }
                    else
                    {
                        await dbController.AddCharacters(voiceActor, characters);
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
            await AddVoiceActors(selectedAnime, await apiController.FindVoiceActors(selectedAnime));
            MessageBox.Show($"{selectedAnime} added to MAL and database", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            doSearch();
        }
        private void tbxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                doSearch();
            }
        }

        private async void doSearch()
        {
            string animeName = tbxSearch.Text;
            List<Anime> animes = await apiController.FindAnime(animeName);

            dgvAnimeList.Rows.Clear();
            foreach (Anime anime in animes)
            {
                dgvAnimeList.Rows.Add(anime.Id, anime.Title);
            }
            dgvAnimeList.ClearSelection();
        }

        private async void CompareButton_Click(object sender, EventArgs e)
        {
            string selectedAnime = (string)dgvAnimeList.SelectedCells[colAnimeName.Index].Value;
            List<VoiceActor> voiceActors = await FindCommonVas(selectedAnime, await FindVoiceActors(selectedAnime));


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

                    Height = 100,

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
                    Image = Image.FromFile("images/default.jpg"), // Replace with actual image path
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Dock = DockStyle.Fill
                };
                tableLayout.Controls.Add(pictureBox, 0, 0);
                if (!string.IsNullOrEmpty(actor.ImageUrl))
                {
                    pictureBox.LoadAsync(actor.ImageUrl);
                }

                // Create and add Label to top-right cell
                string charName = actor.Characters.Where(c => c.AnimeName == selectedAnime).FirstOrDefault()?.CharacterName;
                Label nameLabel = new Label
                {
                    Text = $"{actor.Name} ({charName})",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                //Label thisCharacterLabel = new Label
                //{
                //    Text = actor.Value.,
                //    Dock = DockStyle.Fill,
                //    TextAlign = ContentAlignment.MiddleCenter
                //};
                //tableLayout.Controls.Add(nameLabel, 1, 0);
                tableLayout.Controls.Add(nameLabel, 1, 0);

                // Create and add inner FlowLayoutPanel to bottom cells
                FlowLayoutPanel movieFlowLayout = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false
                };

                string characterString = "";
                foreach (var character in actor.Characters)
                {
                    //FlowLayoutPanel characterFlow = new FlowLayoutPanel
                    //{
                    //    Dock = DockStyle.Fill,
                    //    FlowDirection = FlowDirection.LeftToRight,
                    //    WrapContents = true
                    //};
                    //Label movieLabel = new Label
                    //{
                    //    Text = character.AnimeName,
                    //    Margin = new Padding(5),
                    //    AutoSize = true
                    //};
                    //Label characterLabel = new Label
                    //{
                    //    Text = character.CharacterName,
                    //    ForeColor = Color.Gray,
                    //    Font = new Font("Segoe UI", 6f),
                    //    Margin = new Padding(5, 5, 5, 1),
                    //    AutoSize = true
                    //};
                    //movieFlowLayout.Controls.Add(characterLabel);
                    //movieFlowLayout.Controls.Add(movieLabel);
                    characterString += $" - {character.AnimeName} - {character.CharacterName}\n";
                }
                Label characterLabel = new Label
                {
                    Text = characterString,
                    ForeColor = Color.Black,
                    //Font = new Font("Segoe UI", 6f),
                    Margin = new Padding(5, 5, 5, 1),
                    AutoSize = true
                };
                movieFlowLayout.Controls.Add(characterLabel);

                int initialMovieFlowLayoutHeight = movieFlowLayout.Height;

                // Add FlowLayoutPanel to bottom-left cell
                tableLayout.Controls.Add(movieFlowLayout, 0, 1);
                tableLayout.SetColumnSpan(movieFlowLayout, 2);

                panel.Height = 100 + initialMovieFlowLayoutHeight;

                // Add TableLayoutPanel to the main Panel
                panel.Controls.Add(tableLayout);




                // Add the Panel to the FlowLayoutPanel
                voiceActorFlow.Controls.Add(panel);


            }
        }
    }
}
