using MdtApi.Models;
using MdtApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MdtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficerController : ControllerBase
    {
        private readonly IMongoCollection<Officer> _officerProfiles;

        private readonly OfficerService _mongoDBService;

        public OfficerController(OfficerService mongoDBService, IOptions<MongoDBSettings> settings)
        {
            _mongoDBService = mongoDBService;
            var client = new MongoClient(settings.Value.ConnectionURI);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _officerProfiles = database.GetCollection<Officer>("Officers");
        }

        // Endpoint to get all officer profiles (You can filter and optimize this according to your requirements)
        [HttpGet]
        public async Task<List<Officer>> Get()
        {
            return await _mongoDBService.GetAsync();
        }


        // Endpoint to validate the passcode and return officer profile if valid
        [HttpPost("authorize")]
        public ActionResult<Officer> Authorize([FromBody] string passcode)
        {
            if (passcode == null) { }
            var profile = _officerProfiles.Find(profile => profile.Passcode == passcode).FirstOrDefault();
            return profile;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Officer officer)
        {
            await _mongoDBService.CreateAsync(officer);
            return CreatedAtAction(nameof(Get), new { id = officer.Id }, officer);
        }
    }
}
