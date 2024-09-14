using MongoDB.Bson.Serialization.Attributes;

namespace SeiyuuSync.JsonClasses
{
    class Character
    {
        [BsonElement("name")]
        public string AnimeName { get; set; } = "";
        [BsonElement("character")]
        public string CharacterName { get; set; } = "";
    }
}
