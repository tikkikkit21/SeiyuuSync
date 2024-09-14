using SeiyuuSync.JsonClasses;
using SeiyuuSync.Utils;
using System.Text.Json;

namespace SeiyuuSync
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
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
    }
}
