using SeiyuuSync.JsonClasses;
using System.Text;
using System.Text.Json;

namespace SeiyuuSync.Utils
{
    class ApiController
    {
        private static HttpClient client;

        public ApiController() {
            client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.ACCESS_TOKEN);
        }

        public async Task<AnimeSearchResponse> SearchAnime(string name)
        {
            string query = $"{Constants.MAL_ROOT}/anime?q={name}&limit=5";
            string result = await client.GetStringAsync(query);
            AnimeSearchResponse response = JsonSerializer.Deserialize<AnimeSearchResponse>(result);
            return response;
        }

        public async Task<AnimeSearchResponse> GetMyAnimes()
        {
            string query = $"{Constants.MAL_ROOT}/users/@me/animelist";
            var result = await client.GetStringAsync(query);
            AnimeSearchResponse response = JsonSerializer.Deserialize<AnimeSearchResponse>(result);
            return response;
        }

        public async Task<bool> AddAnime(int animeId)
        {
            string query = $"{Constants.MAL_ROOT}/anime/{animeId}/my_list_status";
            StringContent body = new StringContent("{}", Encoding.UTF8, "application/json");
            var result = await client.PutAsync(query, body);
            result.EnsureSuccessStatusCode();
            return true;
        }

        //public async Task<> FindVoiceActor()

    }
}
