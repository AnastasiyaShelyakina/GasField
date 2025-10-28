using GasField.Data.Services;
using GasField.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GasField.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellsController : ControllerBase
    {
        private readonly IWellsService _service;
        public WellsController(IWellsService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<WellDto>>> GetWells()
        {
            var wells = await _service.GetAll();
            return Ok(wells);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<WellDto>> GetWell(int id)
        {
            var well = await _service.GetById(id);

            if (well == null)
            {
                return NotFound();
            }
            return Ok(well);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Engineer")]
        public async Task<IActionResult> PutWell(int id, UpdateWellDto wellDto)
        {
            var updated = await _service.Update(id, wellDto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpPost]
        [Authorize(Roles = "Engineer")]
        public async Task<ActionResult<WellDto>> PostWell(UpdateWellDto wellDto)
        {
            var well = await _service.Add(wellDto);
            return CreatedAtAction(nameof(GetWell), new { id = well.Id }, well);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Engineer")]
        public async Task<IActionResult> DeleteWell(int id)
        {
            await _service.Delete(id);
            return NoContent();

        }

        ///
        [HttpGet("high-watercut/{ukpgId}/{threshold}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<WellDto>>> GetHighWaterCutByUkpg(int ukpgId, double threshold)
        {
            var wells = await _service.GetHighWaterCutByUkpg(ukpgId, threshold);
            return Ok(wells);
        }

        /*        [HttpGet("top/{count}")]
                public async Task<ActionResult<IEnumerable<WellDto>>> GetTopWells(int count)
                {
                    var wells = await _service.GetTopWellsByExtraction(count);
                    return Ok(wells);
                }*/
        [HttpGet("top-by-ukpg")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<WellDto>>> GetTopWellsByUkpg([FromQuery] int topCount, [FromQuery] int ukpgId)
        {
            var wells = await _service.GetTopWellsByExtraction(topCount, ukpgId);
            return Ok(wells);
        }


    }
}
