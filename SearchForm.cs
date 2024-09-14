using SeiyuuSync.JsonClasses;
using System.Text.Json;

namespace SeiyuuSync
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {

        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string animeName = textBox1.Text;
            string query = $"{Constants.MAL_ROOT}/anime?q={animeName}&limit=5";

            using (HttpClient client = new())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.ACCESS_TOKEN);

                string result = await client.GetStringAsync(query);

                AnimeSearchResponse response = JsonSerializer.Deserialize<AnimeSearchResponse>(result);

                dgvAnimeList.Rows.Clear();
                dgvAnimeList.ClearSelection();
                foreach (Node node in response.Nodes)
                {
                    dgvAnimeList.Rows.Add(node.Anime.Title);
                }
            }
        }
    }
}
