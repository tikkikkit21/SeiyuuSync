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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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
