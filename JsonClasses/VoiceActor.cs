using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace SeiyuuSync.JsonClasses
{
    [DataContract]
    class VoiceActor
    {
        [BsonId]  // Marks this field as the MongoDB "_id" field
        [BsonRepresentation(BsonType.ObjectId)]  // Indicates that the Id will be stored as an ObjectId in MongoDB
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; } = "";
        [BsonElement("animes")]
        public List<Character> Characters { get; set; } = new List<Character>();
    }
}
