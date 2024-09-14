using SeiyuuSync.JsonClasses;
using SeiyuuSync.Utils;
using System.Text.Json;
using System.Windows.Forms;
using System.Collections.Generic;
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

        /// <summary>
        /// Finds all voice actors for a specificed anime
        /// </summary>
        /// <param name="selectedAnime">Name of anime</param>
        /// <returns>Dictionary of VA info</returns>
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
