using Euphorolog.Database.Models;
using Euphorolog.Services.DTOs.UsersDTOs;
using Euphorolog.Services.Services;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<List<UserInfoResponseDTO>>> GetAllUsersAsync()
        {
            return Ok(await _usersService.GetAllUsersAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfoResponseDTO>> GetUserByIdAsync(string id)
        {
            return Ok(await _usersService.GetUserByIdAsync(id));
        }
        //[HttpPost]
        //public async Task<ActionResult<List<Users>>> CreateUsersAsync(Users user)
        //{
        //    return Ok(await _usersService.CreateUserAsync(user));
        //}
        [HttpPut("{id}"),Authorize]
        public async Task<ActionResult<UpdateUserInfoResponseDTO>> UpdateUserAsync([FromRoute] string id, [FromBody] UpdateUserInfoRequestDTO user)
        {
            return Ok(await _usersService.UpdateUserAsync(id, user));
        }
        [HttpDelete("{id}"),Authorize]
        public async Task<ActionResult<List<UserInfoResponseDTO>>> DeleteStoryAsync(string id)
        {
            return Ok(await _usersService.DeleteUserAsync(id));
        }
    }
}
