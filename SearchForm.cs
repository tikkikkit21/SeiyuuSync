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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            ApiController controller = new ApiController();
            await controller.AddAnime(1);




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
            ApiController controller = new ApiController();
            AnimeSearchResponse response = await controller.SearchAnime(animeName);

            dgvAnimeList.Rows.Clear();
            dgvAnimeList.ClearSelection();
            foreach (Node node in response.Nodes)
            {
                dgvAnimeList.Rows.Add(node.Anime.Title);
            }
        }
    }
}
