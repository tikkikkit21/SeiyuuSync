using MongoDB.Driver;
using SeiyuuSync.JsonClasses;
using System.Xml.Linq;

namespace SeiyuuSync.Utils
{
    class DbController
    {
        private static MongoClient dbClient;
        private static IMongoDatabase database;
        public DbController()
        {
            dbClient = new MongoClient(Constants.DB_SRV);
            database = dbClient.GetDatabase("SeiyuuSync");
        }

        /// <summary>
        /// Searches for a VA in the database
        /// </summary>
        /// <param name="name">Name of VA</param>
        /// <returns>VoiceActor if found, null otherwise</returns>
        public async Task<VoiceActor> FindVoiceActor(string name)
        {
            try
            {
                IMongoCollection<VoiceActor> collection = database.GetCollection<VoiceActor>("voice_actors");
                VoiceActor voiceActor = collection
                    .Find(va => va.Name == name)
                    .FirstOrDefault();
                return voiceActor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// Adds a VA to database
        /// </summary>
        /// <param name="va">VoiceActor to add</param>
        /// <returns>Boolean indicating success</returns>
        public async Task<bool> AddVoiceActor(VoiceActor va)
        {
            try
            {
                IMongoCollection<VoiceActor> collection = database.GetCollection<VoiceActor>("voice_actors");
                await collection.InsertOneAsync(va);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }


        public async Task<VoiceActor> AddCharacters(VoiceActor voiceActorToUpdate, List<Character> charactersToAdd)
        {
            IMongoCollection<VoiceActor> collection = database.GetCollection<VoiceActor>("voice_actors");
            VoiceActor voiceActor = collection
                .Find(va => va.Name == voiceActorToUpdate.Name)
                .FirstOrDefault();
            charactersToAdd = charactersToAdd.Where(c => !voiceActor.Characters.Any(vc => CompareCharacter(c, vc))).ToList();
            voiceActor.Characters.AddRange(charactersToAdd);
            var filter = Builders<VoiceActor>.Filter.Eq("name", voiceActorToUpdate.Name);
            await collection.ReplaceOneAsync(filter, voiceActor);
            return voiceActor;
        }

        private bool CompareCharacter(Character c1, Character c2)
        {
            return c1.CharacterName == c2.CharacterName && c1.AnimeName == c2.AnimeName;
        }
    }
}
