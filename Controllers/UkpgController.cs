using GasField.Data.Services;
using GasField.DTOs;
using GasField.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<UkpgDto>>> GetUkpgs()
        {
            var ukpgs = await _service.GetAll();
            return Ok(ukpgs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UkpgDto>> GetUkpg(int id)
        {
            var ukpg= await _service.GetById(id);

            if (ukpg == null) {
                return NotFound();
            }
            return Ok(ukpg);
        }
        [HttpPut("{id}")]

       public async Task<IActionResult> PutUkpg(int id, UkpgDto ukpgDto)
        {
           var ukpg = await _service.Update(id, ukpgDto);
            if (ukpg == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<UkpgDto>> PostUkpg(UkpgDto ukpgDto)
        {
            var ukpg = await _service.Add(ukpgDto);
            return CreatedAtAction("GetUkpg", new { id = ukpg.Id }, ukpgDto);
        }


       [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUkpg(int id)
        {
            await _service.Delete(id);
            return NoContent();

        }
    
}
}
