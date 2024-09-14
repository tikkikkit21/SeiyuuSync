using SeiyuuSync.JsonClasses;
using SeiyuuSync.Utils;
using System.Text.Json;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SeiyuuSync
{
    public partial class SearchForm : Form
    {
        bool split = false;
        public SearchForm()
        {
            InitializeComponent();
        }

        private async void FindVoiceActors()
        {
            string selectedAnime = (string)dgvAnimeList.SelectedCells[colAnimeName.Index].Value;
            ApiController controller = new ApiController();

            // va, char
            Dictionary<string, string> vaDict = await controller.FindAnime(selectedAnime);

            DbController dbController = new DbController();
            foreach (KeyValuePair<string, string> kvp in vaDict)
            {
                string vaName = kvp.Key;
                string charName = kvp.Value;

                if (await dbController.FindVoiceActor(vaName) == null)
                {
                    Character character = new Character
                    {
                        AnimeName = selectedAnime,
                        CharacterName = charName
                    };
                    VoiceActor actor = new VoiceActor
                    {
                        Name = vaName,
                        Characters = [character]
                    };
                    await dbController.AddVoiceActor(actor);
                }
            }
        }

        private async void AddButton_Click(object sender, EventArgs e)
        {
            ApiController controller = new ApiController();
            int animeId = (int)dgvAnimeList.SelectedCells[colAnimeId.Index].Value;
            await controller.AddAnime(animeId);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string animeName = tbxSearch.Text;
            ApiController controller = new ApiController();
            AnimeSearchResponse response = await controller.SearchAnime(animeName);

            dgvAnimeList.Rows.Clear();
            dgvAnimeList.ClearSelection();
            foreach (Node node in response.Nodes)
            {
                dgvAnimeList.Rows.Add(node.Anime.Id, node.Anime.Title);
            }
        }

        private async void CompareButton_Click(object sender, EventArgs e)
        {
            FindVoiceActors();

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


                Dictionary<string, string[]> voiceActorData = new Dictionary<string, string[]>();
                voiceActorData["Timothy Cui"] = new string[] { "Summoner's Rift", "Virginia Tech" };
                voiceActorData["Mike"] = new string[] { "Peraton", "Virginia Tech" };




                TreeView treeView = new TreeView
                {
                    Dock = DockStyle.Fill
                };

                // Add nodes to TreeView
                foreach (var actor in voiceActorData)
                {
                    TreeNode actorNode = new TreeNode(actor.Key);
                    foreach (var movie in actor.Value)
                    {
                        actorNode.Nodes.Add(new TreeNode(movie));
                    }
                    treeView.Nodes.Add(actorNode);
                }

                // Add TreeView to the form
                tableLayoutPanel1.Controls.Add(treeView, 11, 1);
                tableLayoutPanel1.SetColumnSpan(treeView, 8);

                treeView.ExpandAll();

                split = true;
            }
        }
    }
}
