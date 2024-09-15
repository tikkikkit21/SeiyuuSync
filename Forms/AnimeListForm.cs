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
            Cursor cursor = Cursor.Current;
            Cursor = Cursors.WaitCursor;
            List<Anime> animes = await apiController.GetMyAnimes();
            foreach (Anime anime in animes)
            {
                dgvAnimeList.Rows.Add(anime.Title);
            }
            dgvAnimeList.ClearSelection();
            Cursor = cursor;
        }
    }
}
