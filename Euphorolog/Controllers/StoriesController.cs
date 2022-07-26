﻿using Euphorolog.Database.Models;
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
        private readonly IUriService _uriService;
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
            var totalRecords = await _storiesService.GetTotalStoryCountAsync();
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
            var ret = await _storiesService.PostStoryAsync(story);
            return Ok(new StoryResponse<GetStoryByIdResponseDTO>(ret));
        }
        [HttpPut("{id}"),Authorize]
        public async Task<ActionResult<GetStoryByIdResponseDTO>> UpdateStoryAsync([FromRoute] string id, [FromBody] UpdateStoryRequestDTO story)
        {
            var ret = await _storiesService.UpdateStoryAsync(id, story);
            return Ok(new StoryResponse<GetStoryByIdResponseDTO>(ret));
        }
        [HttpDelete("{id}"),Authorize]
        public async Task<ActionResult<PagedResponse<List<GetAllStoriesResponseDTO>>>> DeleteStoryAsync(string id)
        {
            await _storiesService.DeleteStoryAsync(id);

            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(1, 10);
            var pagedData = await _storiesService.GetAllStoriesAsync(1, 10);
            var totalRecords = await _storiesService.GetTotalStoryCountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetAllStoriesResponseDTO>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// the only path content negotiation is applied
        [HttpGet("{id}/download")]
        public async Task<ActionResult<GetStoryByIdResponseDTO>> GetStoryByIdDownloadAsync(string id)
        {
            var ret = await _storiesService.GetStoryByIdAsync(id);
            return Ok(ret);
        }
    }
}
