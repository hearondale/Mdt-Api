using MdtApi.Models;
using MdtApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MdtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly CitizenService _mongoDBService;

        public CitizenController(CitizenService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }
        [HttpGet]
        public async Task<List<Citizen>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Citizen citizen)
        {
            await _mongoDBService.CreateAsync(citizen);
            return CreatedAtAction(nameof(Get), new { id = citizen.Id }, citizen);
        }

        [HttpPut("{name}, {value}")]
        public async Task<IActionResult> PutToDoc(string name, string value)
        {
            await _mongoDBService.AddParamsAsync(name, value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }
}
