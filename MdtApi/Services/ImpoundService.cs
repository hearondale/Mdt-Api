using MdtApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Globalization;

namespace MdtApi.Services
{
    public class ImpoundService
    {
        private readonly IMongoCollection<Impound> _impoundCollection;
        public ImpoundService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _impoundCollection = database.GetCollection<Impound>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<Impound>> GetAsync() {
            return await _impoundCollection.Find(new BsonDocument())
                .SortByDescending(Impound => Impound.CustomId).ToListAsync();
        }
        public async Task<List<Impound>> GetByIdAsync(string id) {
            FilterDefinition<Impound> filter = Builders<Impound>.Filter.Eq("Owner.Id", id) & Builders<Impound>.Filter.Eq("Status", "Конфисковано"); ;
            return await _impoundCollection.Find(filter)
                .SortByDescending(Impound => Impound.CustomId).ToListAsync();
        }
        public async Task CreateAsync(Impound impound) {
            await _impoundCollection.InsertOneAsync(impound);
            return;
        }

        public async Task UpdateDataAsync(string id, Impound value) {
            FilterDefinition<Impound> filter = Builders<Impound>.Filter.Eq("Id", id);
            var options = new UpdateOptions { IsUpsert = true };
            await _impoundCollection.ReplaceOneAsync(filter, value);
            return;
        }   

        public async Task DeleteAsync(string id) {
            FilterDefinition<Impound> filter = Builders<Impound>.Filter.Eq("Id", id);
            await _impoundCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
