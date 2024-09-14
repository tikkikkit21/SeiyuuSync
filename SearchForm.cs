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
        bool split = false;
        public SearchForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Finds all common VAs. This method takes in a dictionary of VAs and compares it with all VAs in the database
        /// </summary>
        /// <param name="animeName">Name of anime</param>
        /// <param name="vaDict">Dictionary of all VAs for this anime</param>
        /// <returns>Dictionary of common VAs</returns>
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

        /// <summary>
        /// Adds all VAs in dictionary to MAL and DB
        /// </summary>
        /// <param name="animeName">Name of anime</param>
        /// <param name="vaDict">Dictionary of VA info</param>
        private async void AddVoiceActors(string animeName, Dictionary<string, List<string>> vaDict)
        {
            try
            {
                DbController dbController = new DbController();
                foreach (KeyValuePair<string, List<string>> kvp in vaDict)
                {
                    string vaName = kvp.Key;
                    List<string> characters = kvp.Value;

                    if (await dbController.FindVoiceActor(vaName) == null)
                    {
                        List<Character> list = characters.Select(c => new Character { AnimeName = animeName, CharacterName = c }).ToList();
                        VoiceActor actor = new VoiceActor
                        {
                            Name = vaName,
                            Characters = list
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
        private async Task<Dictionary<string, List<string>>?> FindVoiceActors(string selectedAnime)
        {
            try
            {
                ApiController controller = new ApiController();

                // va, [char]
                Dictionary<string, List<string>> vaDict = await controller.FindVoiceActors(selectedAnime);
                //AddVoiceActors(selectedAnime, vaDict);
                return vaDict;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
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
            FindCommonVas(selectedAnime, await FindVoiceActors(selectedAnime));

            if (!split)
            {
                tableLayoutPanel1.Controls.Remove(dgvAnimeList);
                tableLayoutPanel1.Controls.Add(dgvAnimeList, 1, 1); // Move button to cell (2,2)
                tableLayoutPanel1.SetColumnSpan(dgvAnimeList, 10);






                //tableLayoutPanel1.Controls.Remove(dvgVoiceActors);
                //tableLayoutPanel1.Controls.Add(dvgVoiceActors, 11, 1); // Move button to cell (2,2)
                //tableLayoutPanel1.SetColumnSpan(dvgVoiceActors, 8);
                //dvgVoiceActors.Visible = true;

                int animeId = (int)dgvAnimeList.SelectedCells[colAnimeId.Index].Value;
                //api stuff here where you fetch the voice actors associated with this anime (locally i think)


                Dictionary<string, MovieCharacter[]> voiceActors = new Dictionary<string, MovieCharacter[]>();
                //voiceActorData["Timothy Cui"] = new string[] { "Summoner's Rift", "Virginia Tech" };
                //voiceActorData["Mike"] = new string[] { "Peraton", "Virginia Tech" };

                voiceActors.Add("Timothy Cui", new[]
                {
                new MovieCharacter("Summoner's Rift", "JadeJaguar"),
                new MovieCharacter("Virginia Tech", "Different"),
                new MovieCharacter("Chi Alpha", "Grad"),
            });

                voiceActors.Add("Jane Smith", new[]
                {
                new MovieCharacter("Frozen", "Elsa"),
                new MovieCharacter("Enchanted", "Nancy Tremaine")
            });

                FlowLayoutPanel voiceActorFlow = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true, // Enable automatic scrolling
                    FlowDirection = FlowDirection.TopDown, // Stack panels vertically
                    WrapContents = false // Prevent wrapping, keep adding vertically
                };

                tableLayoutPanel1.Controls.Add(voiceActorFlow, 11, 1);
                tableLayoutPanel1.SetColumnSpan(voiceActorFlow, 8);

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

                    foreach (var movie in actor.Value)
                    {
                        Label movieLabel = new Label
                        {
                            Text = movie.Movie,
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



















                    //TableLayoutPanel panelGrid = new TableLayoutPanel
                    //{
                    //    Dock = DockStyle.Fill,
                    //    Size = new System.Drawing.Size(voiceActorFlow.Width, 200),
                    //    ColumnCount = 2,
                    //    RowCount = 2,
                    //    RowStyles = { new RowStyle(SizeType.Percent, 50), new RowStyle(SizeType.Percent, 50) },
                    //    ColumnStyles = { new ColumnStyle(SizeType.Percent, 50), new ColumnStyle(SizeType.Percent, 50) }
                    //};

                    //Label nameLabel = new Label
                    //{
                    //    Text = actor.Key,
                    //    AutoSize = true,
                    //    Dock = DockStyle.Right,
                    //};

                    //panelGrid.Controls.Add(nameLabel, 1, 0); // Label in the top right




                    //// Initialize the PictureBox
                    //PictureBox pictureBox = new PictureBox
                    //{
                    //    //Image = Image.FromFile(@"images\ToyokawaShrine.png"), // Replace with the path to your image file
                    //    SizeMode = PictureBoxSizeMode.AutoSize, // Adjust size mode as needed
                    //    Dock = DockStyle.Left
                    //};

                    //FlowLayoutPanel animesFlow = new FlowLayoutPanel
                    //{
                    //    Dock = DockStyle.Fill,
                    //    AutoScroll = true,
                    //    FlowDirection = FlowDirection.TopDown,
                    //    WrapContents = false
                    //};

                    //// Add controls to the Panel
                    //panelGrid.Controls.Add(pictureBox, 0, 0); // PictureBox in the top left
                    //panelGrid.Controls.Add(nameLabel, 1, 0); // Label in the top right
                    //panelGrid.Controls.Add(animesFlow, 0, 1); // FlowLayoutPanel in the bottom row (spanning both columns)
                    //panelGrid.SetColumnSpan(animesFlow, 2); // Span across both columns


                    //foreach (var animeCharacter in actor.Value)
                    //{
                    //    Label animeLabel = new Label
                    //    {
                    //        Text = animeCharacter.Movie,
                    //        AutoSize = true,
                    //        Dock = DockStyle.Left,
                    //        Padding = new Padding(0, 0, 10, 0) // Add some padding to separate from the PictureBox
                    //    };
                    //    animesFlow.Controls.Add(animeLabel);
                    //}




                    //voiceActorFlow.Controls.Add(nameLabel);


                }




                //TreeView treeView = new TreeView
                //{
                //    Dock = DockStyle.Fill
                //};

                //// Add nodes to TreeView
                //foreach (var actor in voiceActorData)
                //{
                //    TreeNode actorNode = new TreeNode(actor.Key);
                //    foreach (var movie in actor.Value)
                //    {
                //        actorNode.Nodes.Add(new TreeNode(movie));
                //    }
                //    treeView.Nodes.Add(actorNode);
                //}

                // Add TreeView to the form
                //tableLayoutPanel1.Controls.Add(treeView, 11, 1);
                //tableLayoutPanel1.SetColumnSpan(treeView, 8);

                //treeView.ExpandAll();

                split = true;
            }
        }
    }
}

public class MovieCharacter
{
    public string Movie { get; set; }
    public string Character { get; set; }

    public MovieCharacter(string movie, string character)
    {
        Movie = movie;
        Character = character;
    }

    public override string ToString()
    {
        return $"Movie: {Movie}, Character: {Character}";
    }
}

