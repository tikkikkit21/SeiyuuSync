using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace SeiyuuSync.JsonClasses
{
    [DataContract]
    class VoiceActor
    {
        [BsonId]
        public long Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; } = "";
        [BsonElement("animes")]
        public List<Character> Characters { get; set; } = new List<Character>();
    }
}
