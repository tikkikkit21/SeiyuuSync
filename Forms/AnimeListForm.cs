using SeiyuuSync.JsonClasses;
using SeiyuuSync.Utils;

namespace SeiyuuSync.Forms
{
    public partial class AnimeListForm : Form
    {
        private static ApiController apiController;
        public AnimeListForm()
        {
            apiController = new ApiController();
            InitializeComponent();
        }

        private async void AnimeListForm_Load(object sender, EventArgs e)
        {
            List<Anime> animes = await apiController.GetMyAnimes();
            foreach (Anime anime in animes)
            {
                dgvAnimeList.Rows.Add(anime.Title);
            }
        }
    }
}
