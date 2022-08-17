using Euphorolog.Database.Models;
using Euphorolog.Services.DTOs.UsersDTOs;
using Euphorolog.Services.Services;
using Euphorolog.Wrappers;
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
        public async Task<ActionResult<UserResponse<List<UserInfoResponseDTO>>>> GetAllUsersAsync()
        {
            var ret = await _usersService.GetAllUsersAsync();
            return Ok(new UserResponse<List<UserInfoResponseDTO>>(ret));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse<UserInfoResponseDTO>>> GetUserByIdAsync(string id)
        {
            var ret = await _usersService.GetUserByIdAsync(id);
            return Ok(new UserResponse<UserInfoResponseDTO>(ret));
        }

        [HttpPut("{id}"),Authorize]
        public async Task<ActionResult<UserResponse<UpdateUserInfoResponseDTO>>> UpdateUserAsync([FromRoute] string id, [FromBody] UpdateUserInfoRequestDTO user)
        {
            var ret = await _usersService.UpdateUserAsync(id, user);
            return Ok(new UserResponse<UpdateUserInfoResponseDTO>(ret));
        }

        [HttpDelete("{id}"),Authorize]
        public async Task DeleteStoryAsync(string id)
        {
            await _usersService.DeleteUserAsync(id);
            return;
        }
    }
}
