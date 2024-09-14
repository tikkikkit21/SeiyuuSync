using MongoDB.Driver;
using SeiyuuSync.JsonClasses;

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
        public async Task<bool> AddVoiceActor(VoiceActor va) {
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
    }
}
