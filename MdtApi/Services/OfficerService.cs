using MdtApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace MdtApi.Services
{
    public class OfficerService
    {
        private readonly IMongoCollection<Officer> _officerCollection;
        public OfficerService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _officerCollection = database.GetCollection<Officer>("Officers");
        }

        public async Task<List<Officer>> GetAsync()
        {
            return await _officerCollection.Find(new BsonDocument())
                .SortByDescending(Citizen => Citizen.CustomId).ToListAsync();
        }
        public async Task CreateAsync(Officer officer)
        {
            await _officerCollection.InsertOneAsync(officer);
            return;
        }
    }
}
