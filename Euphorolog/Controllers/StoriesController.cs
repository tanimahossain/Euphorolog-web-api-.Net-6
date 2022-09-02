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
    [Route("api/[controller]")]
    [ApiController] 
    public class StoriesController : ControllerBase
    {
        private readonly IStoriesService _storiesService;
        public StoriesController(
            IStoriesService storiesService
        )
        {
            _storiesService = storiesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStoriesAsync([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.pageNumber, filter.pageSize);
            var pagedData = await _storiesService.GetAllStoriesAsync(validFilter.pageNumber, validFilter.pageSize);
            var totalRecords = await _storiesService.GetTotalStoryCountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetStoryResponseDTO>(pagedData, validFilter, totalRecords);
            return Ok(pagedReponse);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoryByIdAsync(string id)
        {
            var ret = await _storiesService.GetStoryByIdAsync(id);
            return Ok(new StoryResponse<GetStoryResponseDTO>(ret));
        }
        
        [HttpPost,Authorize]
        public async Task<IActionResult> PostStoryAsync(PostStoryRequestDTO story)
        {
            var ret = await _storiesService.PostStoryAsync(story);
            return Ok(new StoryResponse<GetStoryResponseDTO>(ret));
        }
        
        [HttpPut("{id}"),Authorize]
        public async Task<IActionResult> UpdateStoryAsync([FromRoute] string id, [FromBody] UpdateStoryRequestDTO story)
        {
            var ret = await _storiesService.UpdateStoryAsync(id, story);
            return Ok(new StoryResponse<GetStoryResponseDTO>(ret));
        }
        
        [HttpDelete("{id}"),Authorize]
        public async Task<IActionResult> DeleteStoryAsync(string id)
        {
            await _storiesService.DeleteStoryAsync(id);
            var validFilter = new PaginationFilter(1, 10);
            var pagedData = await _storiesService.GetAllStoriesAsync(1, 10);
            var totalRecords = await _storiesService.GetTotalStoryCountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetStoryResponseDTO>(pagedData, validFilter, totalRecords);
            return Ok(pagedReponse);
        }
        
        /// the only path content negotiation is applied
        [HttpGet("{id}/download")]
        public async Task<IActionResult> GetStoryByIdDownloadAsync(string id)
        {
            var ret = await _storiesService.GetStoryByIdAsync(id);
            return Ok(ret);
        }
    }
}
