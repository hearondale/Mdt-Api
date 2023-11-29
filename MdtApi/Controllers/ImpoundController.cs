using MdtApi.Models;
using MdtApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MdtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImpoundController : ControllerBase
    {
        private readonly ImpoundService _mongoDBService;

        public ImpoundController(ImpoundService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }



        [HttpGet]
        public async Task<List<Impound>> Get()
        {
            return await _mongoDBService.GetAsync();
        }
        [HttpGet("{id}")]
        public async Task<List<Impound>> GetForCitizen(string id)
        {
            return await _mongoDBService.GetByIdAsync(id);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Impound impound)
        {
            List<Impound> resultList = await _mongoDBService.GetAsync();
            impound.CustomId = resultList[0].CustomId + 1;
            await _mongoDBService.CreateAsync(impound);
            return CreatedAtAction(nameof(Get), new { id = impound.Id }, impound);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoc(string id, [FromBody] Impound value)
        {
            await _mongoDBService.UpdateDataAsync(id, value);
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
