using GasField.Data.Services;
using GasField.DTOs;
using GasField.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace GasField.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UkpgController : ControllerBase
    {
        private readonly IUkpgsService _service;
        public UkpgController(IUkpgsService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UkpgDto>>> GetUkpgs()
        {
            var ukpgs = await _service.GetAll();
            return Ok(ukpgs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UkpgDto>> GetUkpg(int id)
        {
            var ukpg= await _service.GetById(id);

            if (ukpg == null) {
                return NotFound();
            }
            return Ok(ukpg);
        }
        
        [HttpPost]
        [Authorize(Roles = "Engineer")]
        public async Task<ActionResult<UkpgDto>> PostUkpg(UpdateUkpgDto ukpgDto)
        {
            var ukpg = await _service.Add(ukpgDto);
            return CreatedAtAction(nameof(GetUkpg), new { id = ukpg.Id }, ukpg);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Engineer")]
        public async Task<IActionResult> PutUkpg(int id, UpdateUkpgDto ukpgDto)
        {
            var ukpg = await _service.Update(id, ukpgDto);
            if (ukpg == null)
                return NotFound();

            return Ok(ukpg);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Engineer")]
        public async Task<IActionResult> DeleteUkpg(int id)
        {
            await _service.Delete(id);
            return NoContent();

        }

    
}
}
