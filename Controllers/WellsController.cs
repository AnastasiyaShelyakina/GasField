using GasField.Data.Services;
using GasField.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<WellDto>>> GetWells()
        {
            var wells = await _service.GetAll();
            return Ok(wells);
        }

        [HttpGet("{id}")]
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

      public async Task<IActionResult> PutWell(int id, WellDto wellDto)
        {
            var wel = await _service.Update(id, wellDto);
            if (wel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<WellDto>> PostWell(WellDto wellDto)
        {
            var well = await _service.Add(wellDto);
            return CreatedAtAction("GetWell", new { id = well.Id }, wellDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWell(int id)
        {
            await _service.Delete(id);
            return NoContent();

        }

    }
}
