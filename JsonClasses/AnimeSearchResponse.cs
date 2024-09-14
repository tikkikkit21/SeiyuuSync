using System.Text.Json.Serialization;

namespace SeiyuuSync.JsonClasses
{
    class AnimeSearchResponse
    {
        [JsonPropertyName("data")]
        public List<Node> Data { get; set; } = new List<Node>();
    }

    public class Node
    {
        [JsonPropertyName("node")]
        public Anime Anime { get; set; }
    }

    public class Anime
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
