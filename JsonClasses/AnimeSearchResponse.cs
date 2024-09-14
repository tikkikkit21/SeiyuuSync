using System.Text.Json.Serialization;

namespace SeiyuuSync.JsonClasses
{
    class AnimeSearchResponse
    {
        [JsonPropertyName("data")]
        public List<Node> Nodes { get; set; } = new List<Node>();
    }
}
