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

        public async Task<VoiceActor> FindVoiceActor(string name)
        {
            IMongoCollection<VoiceActor> collection = database.GetCollection<VoiceActor>("voice_actors");
            VoiceActor result = collection.Find(va => va.Name == name).FirstOrDefault();
            return result;
        }

        public async Task<bool> AddVoiceActor(VoiceActor va) {
            IMongoCollection<VoiceActor> collection = database.GetCollection<VoiceActor>("voice_actors");
            await collection.InsertOneAsync(va);
            return true;
        }
    }
}
