using Euphorolog.Database.Models;
using Euphorolog.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Euphorolog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUsersService _usersService;
        public UsersController(IUsersService storiesService)
        {
            _usersService = storiesService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetAsync()
        {
            return Ok(await _usersService.GetAllUsersAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetAsync(string id)
        {
            return Ok(await _usersService.GetUserByIdAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult<List<Users>>> CreateUsersAsync(Users user)
        {
            return Ok(await _usersService.CreateUserAsync(user));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> Put([FromRoute] string id, [FromBody] Users user)
        {
            return Ok(await _usersService.UpdateUserAsync(id, user));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Users>>> DeleteStoryAsync(string id)
        {
            return Ok(await _usersService.DeleteUserAsync(id));
        }
    }
}
