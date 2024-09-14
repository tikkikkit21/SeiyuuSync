using System.Text.Json.Serialization;

namespace SeiyuuSync.JsonClasses
{
    public class Anime
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";
    }
}
