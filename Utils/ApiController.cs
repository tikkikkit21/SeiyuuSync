using Amazon.Runtime.Internal.Transform;
using SeiyuuSync.JsonClasses;
using System.Text;
using System.Text.Json;

namespace SeiyuuSync.Utils
{
    class ApiController
    {
        /// <summary>
        /// Client to make HTTP requests for MAL API
        /// </summary>
        private static HttpClient _mal_client;

        /// <summary>
        /// Client to make HTTP requests for AniList API
        /// </summary>
        private static HttpClient _anilist_client;

        public ApiController() {
            _mal_client = new HttpClient();
            _mal_client.BaseAddress = new Uri(Constants.MAL_ROOT);
            _mal_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.ACCESS_TOKEN);

            _anilist_client = new HttpClient();
            _anilist_client.BaseAddress = new Uri(Constants.ANILIST_ROOT);
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
            string query = $"anime?q={name}&limit={limit}";

            try
            {
                string result = await _mal_client.GetStringAsync(query);
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
                string query = $"users/@me/animelist";
                var result = await _mal_client.GetStringAsync(query);
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
                string query = $"anime/{animeId}/my_list_status";
                StringContent body = new StringContent("{}", Encoding.UTF8, "application/json");
                var result = await _mal_client.PutAsync(query, body);
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
        public async Task<List<VoiceActor>> FindVoiceActors(string animeName)
        {
            List<VoiceActor> vaList = new List<VoiceActor>();
            try
            {
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
                          image {
                            medium
                          }
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
                HttpResponseMessage response = await _anilist_client.PostAsync("/", content);
                response.EnsureSuccessStatusCode();

                // Read the response body as a string
                string jsonResponse = await response.Content.ReadAsStringAsync();
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
                        var voiceActors = characterEdge.GetProperty("voiceActors").EnumerateArray();
                        var japaneseVa = voiceActors.Where(va => va.GetProperty("language").GetString().ToUpper() == "JAPANESE").FirstOrDefault();
                        string voiceActorName = japaneseVa.GetProperty("name").GetProperty("full").GetString();
                        string imageUrl = japaneseVa.GetProperty("image").GetProperty("medium").GetString();

                        VoiceActor storedVa = vaList.Where(va => va.Name == voiceActorName).FirstOrDefault();
                        if (storedVa == null)
                        {
                            storedVa = new VoiceActor
                            {
                                Name = voiceActorName,
                                ImageUrl = imageUrl,
                                Characters = new List<Character>
                                {
                                    new Character
                                    {
                                        AnimeName = animeName,
                                        CharacterName = characterName
                                    }
                                }
                            };
                            vaList.Add(storedVa);
                        }
                        else
                        {
                            storedVa.Characters.Add(
                                new Character
                                {
                                    AnimeName = animeName,
                                    CharacterName = characterName
                                }
                            );
                        }
                        //foreach (JsonElement voiceActor in voiceActors.EnumerateArray())
                        //{
                        //    string language = voiceActor.GetProperty("language").GetString();

                        //    // Filter for Japanese voice actors
                        //    if (language.ToUpper() == "JAPANESE")
                        //    {
                        //        string voiceActorName = voiceActor.GetProperty("name").GetProperty("full").GetString();
                        //        string imageUrl = voiceActor.GetProperty("image").GetProperty("medium").GetString();
                        //        if (vaList.ContainsKey(voiceActorName))
                        //        {
                        //            vaList[voiceActorName].Add(characterName);
                        //        }
                        //        else
                        //        {
                        //            vaList.Add(voiceActorName, new List<string> { characterName });
                        //        }
                        //        break;
                        //    }
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return vaList;
        }
    }
}
