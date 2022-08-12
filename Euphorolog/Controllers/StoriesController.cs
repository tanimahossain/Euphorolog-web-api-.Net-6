using Euphorolog.Database.Models;
using Euphorolog.Services.DTOs.StoriesDTOs;
using Euphorolog.Filter;
using Euphorolog.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Euphorolog.Helpers;
using Euphorolog.Services.Contracts;
using Euphorolog.Wrappers;

namespace Euphorolog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController] 
    public class StoriesController : ControllerBase
    {
        public readonly IStoriesService _storiesService;
        public readonly IUriService _uriService;
        public StoriesController(
            IStoriesService storiesService,
            IUriService UriService
        )
        {
            _storiesService = storiesService;
            _uriService = UriService;
        }
        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<GetAllStoriesResponseDTO>>>> GetAllStoriesAsync([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.pageNumber, filter.pageSize);
            var pagedData = await _storiesService.GetAllStoriesAsync(filter.pageNumber, filter.pageSize);
            var totalRecords = await _storiesService.TotalStoryNoAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetAllStoriesResponseDTO>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StoryResponse<GetStoryByIdResponseDTO>>> GetStoryByIdAsync(string id)
        {
            var ret = await _storiesService.GetStoryByIdAsync(id);
            return Ok(new StoryResponse<GetStoryByIdResponseDTO>(ret));
        }
        [HttpPost,Authorize]
        public async Task<ActionResult<GetStoryByIdResponseDTO>> PostStoryAsync(PostStoryRequestDTO story)
        {
            return Ok(await _storiesService.PostStoryAsync(story));
        }
        [HttpPut("{id}"),Authorize]
        public async Task<ActionResult<GetStoryByIdResponseDTO>> Put([FromRoute] string id, [FromBody] UpdateStoryRequestDTO story)
        {
            return Ok(await _storiesService.UpdateStoryAsync(id, story));
        }
        [HttpDelete("{id}"),Authorize]
        public async Task<ActionResult<List<GetAllStoriesResponseDTO>>> DeleteStoryAsync(string id)
        {
            return Ok(await _storiesService.DeleteStoryAsync(id));
        }
    }
}
