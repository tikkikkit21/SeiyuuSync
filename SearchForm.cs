﻿using SeiyuuSync.JsonClasses;
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
            await controller.GetMyAnimes();
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
    }
}
