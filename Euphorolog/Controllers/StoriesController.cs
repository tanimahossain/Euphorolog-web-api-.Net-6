using Euphorolog.Database.Models;
using Euphorolog.Services.DTOs;
using Euphorolog.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Euphorolog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController] 
    public class StoriesController : ControllerBase
    {
        public readonly IStoriesService _storiesService;
        public StoriesController (IStoriesService storiesService)
        {
            _storiesService = storiesService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Stories>>> GetAsync()
        {
            return Ok(await _storiesService.GetAllStoriesAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StoriesDTO>> GetAsync(string id)
        {
            return Ok(await _storiesService.GetStoryByIdAsync(id));
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<List<Stories>>> PostStoryAsync(Stories story)
        {
            return Ok(await _storiesService.PostStoryAsync(story));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Stories>> Put([FromRoute] string id, [FromBody] Stories story)
        {
            return Ok(await _storiesService.UpdateStoryAsync(id, story));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Stories>>> DeleteStoryAsync(string id)
        {
            return Ok(await _storiesService.DeleteStoryAsync(id));
        }
    }
}
