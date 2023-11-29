using MdtApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Globalization;

namespace MdtApi.Services
{
    public class CitizenService
    {
        private readonly IMongoCollection<Citizen> _citizenCollection;
        public CitizenService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _citizenCollection = database.GetCollection<Citizen>("Citizen");
        }

        public async Task<List<Citizen>> GetAsync()
        {
            return await _citizenCollection.Find(new BsonDocument())
                .SortByDescending(Citizen => Citizen.CustomId).ToListAsync();
        }

        public async Task CreateAsync(Citizen citizen)
        {
            await _citizenCollection.InsertOneAsync(citizen);
            return;
        }

        public async Task AddParamsAsync(string name, string value)
        {
            var filter = Builders<Citizen>.Filter.Empty;
            var update = Builders<Citizen>.Update.Set(name, value);
            var options = new UpdateOptions { IsUpsert = true };
            _citizenCollection.UpdateMany(filter, update, options);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Citizen> filter = Builders<Citizen>.Filter.Eq("Id", id);
            await _citizenCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
