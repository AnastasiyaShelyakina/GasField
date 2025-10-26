using GasField.Data.Services;
using GasField.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GasField.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service) 
        {
            _service=  service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserUNDto>>> GetUsers()
        {
            var users = await _service.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserUNDto>> GetUser(int id)
        { 
            var user = await _service.GetById(id);
            if (user == null) 
            {
                return NotFound();
            }
            return Ok(user);    
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutUser(int id, UserDTO userDto)
        {
            var user = await _service.Update(id, userDto);
            if (user == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDto)
        {
            var user = await _service.Add(userDto);
            return CreatedAtAction("GetUser", new { id = user.Id }, userDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _service.Delete(id);
            return NoContent();

        }
    }
}
