using Amazon.Runtime.Internal.Transform;
using SeiyuuSync.JsonClasses;
using System.Text;
using System.Text.Json;

namespace SeiyuuSync.Utils
{
    class ApiController
    {
        /// <summary>
        /// Client to make HTTP requests
        /// </summary>
        private static HttpClient client;

        public ApiController() {
            client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.ACCESS_TOKEN);
        }

        /// <summary>
        /// Searches MAL API for anime
        /// </summary>
        /// <param name="name">Name of anime to query</param>
        /// <param name="limit">Number of results to display (default 10)</param>
        /// <returns>AnimeSearchResponse mirroring the JSON structure</returns>
        public async Task<List<Anime>> FindAnime(string name, int limit = 10)
        {
            List<Anime> animes = new List<Anime>();
            string query = $"{Constants.MAL_ROOT}/anime?q={name}&limit={limit}";

            try
            {
                string result = await client.GetStringAsync(query);
                AnimeSearchResponse response = JsonSerializer.Deserialize<AnimeSearchResponse>(result);
                
                if (response != null)
                {
                    foreach (Node node in response.Nodes)
                    {
                        animes.Add(node.Anime);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return animes;
        }

        /// <summary>
        /// Gets my personal MAL list of animes
        /// </summary>
        /// <returns>AnimeSearchResponse mirroring the JSON structure</returns>
        public async Task<List<Anime>> GetMyAnimes()
        {
            List<Anime> animes = new List<Anime>();

            try
            {
                string query = $"{Constants.MAL_ROOT}/users/@me/animelist";
                var result = await client.GetStringAsync(query);
                AnimeSearchResponse response = JsonSerializer.Deserialize<AnimeSearchResponse>(result);

                if (response != null)
                {
                    foreach (Node node in response.Nodes)
                    {
                        animes.Add(node.Anime);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return animes;
        }

        /// <summary>
        /// Adds an anime to my personal MAL list of animes
        /// </summary>
        /// <param name="animeId">ID of anime</param>
        /// <returns>Boolean indicating success</returns>
        public async Task<bool> AddAnime(int animeId)
        {
            try
            {
                string query = $"{Constants.MAL_ROOT}/anime/{animeId}/my_list_status";
                StringContent body = new StringContent("{}", Encoding.UTF8, "application/json");
                var result = await client.PutAsync(query, body);
                result.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// Finds all voice actors for an anime
        /// </summary>
        /// <param name="animeName">Name of anime</param>
        /// <returns>Dictionary of VA names associated with a list of characters</returns>
        public async Task<Dictionary<string, List<string>>> FindVoiceActors(string animeName)
        {
            try
            {
                client.DefaultRequestHeaders.Clear();
                string query = @"
                query ($search: String) {
                  Media(search: $search, type: ANIME) {
                    id
                    title {
                      romaji
                      english
                    }
                    characters {
                      edges {
                        node {
                          name {
                            full
                          }
                        }
                        voiceActors {
                          name {
                            full
                          }
                          language
                        }
                      }
                    }
                  }
                }";

                // Wrap the query and variables into a single request payload
                var variables = new { search = animeName };
                var requestPayload = new
                {
                    query,
                    variables
                };

                // Serialize the request payload to JSON
                string jsonPayload = JsonSerializer.Serialize(requestPayload);
                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(Constants.ANILIST_ROOT, content);
                response.EnsureSuccessStatusCode();

                // Read the response body as a string
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Dictionary<string, List<string>> vaDict = new Dictionary<string, List<string>>();
                using (JsonDocument document = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = document.RootElement;

                    // Navigate to the relevant section of the JSON response
                    JsonElement media = root.GetProperty("data").GetProperty("Media");

                    // Navigate to the characters and iterate over them
                    JsonElement characters = media.GetProperty("characters").GetProperty("edges");

                    foreach (JsonElement characterEdge in characters.EnumerateArray())
                    {
                        JsonElement characterNode = characterEdge.GetProperty("node");
                        string characterName = characterNode.GetProperty("name").GetProperty("full").GetString();

                        // Get the voice actors for this character
                        JsonElement voiceActors = characterEdge.GetProperty("voiceActors");
                        foreach (JsonElement voiceActor in voiceActors.EnumerateArray())
                        {
                            string language = voiceActor.GetProperty("language").GetString();

                            // Filter for Japanese voice actors
                            if (language.ToUpper() == "JAPANESE")
                            {
                                string voiceActorName = voiceActor.GetProperty("name").GetProperty("full").GetString();
                                if (vaDict.ContainsKey(voiceActorName))
                                {
                                    vaDict[voiceActorName].Add(characterName);
                                }
                                else
                                {
                                    vaDict.Add(voiceActorName, new List<string> { characterName });
                                }
                                break;
                            }
                        }
                    }
                }

                return vaDict;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
