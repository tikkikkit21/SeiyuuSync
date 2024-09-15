using SeiyuuSync.Forms;

namespace SeiyuuSync
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            new SearchForm().Show();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            (new AnimeListForm()).Show();
        }
    }
}
