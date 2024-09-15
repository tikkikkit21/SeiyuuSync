namespace SeiyuuSync
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            // Create an instance of the second form
            SearchForm searchForm = new SearchForm();

            // Show the second form
            searchForm.Show(); // Use ShowDialog() if you want the second form to be modal
        }

        private void btnList_Click(object sender, EventArgs e)
        {

        }
    }
}
