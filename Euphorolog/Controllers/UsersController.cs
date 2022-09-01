using Euphorolog.Filter;
using Euphorolog.Helpers;
using Euphorolog.Services.Contracts;
using Euphorolog.Services.DTOs.StoriesDTOs;
using Euphorolog.Services.DTOs.UsersDTOs;
using Euphorolog.Services.Services;
using Euphorolog.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Euphorolog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IStoriesService _storiesService;
        public UsersController(IUsersService usersService, IStoriesService storiesService)
        {
            _usersService = usersService;
            _storiesService = storiesService;
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
        [HttpGet("{username}/stories")]
        public async Task<ActionResult<PagedResponse<List<GetStoryResponseDTO>>>> GetStoriesByUserIdAsync(string username, [FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.pageNumber, filter.pageSize);
            var pagedData = await _storiesService.GetStoriesByUserIdAsync(filter.pageNumber, filter.pageSize,username);
            var totalRecords = await _storiesService.GetTotalStoryCountOfAUserAsync(username);
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetStoryResponseDTO>(pagedData, validFilter, totalRecords);
            return Ok(pagedReponse);
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
