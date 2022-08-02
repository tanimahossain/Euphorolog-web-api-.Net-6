using AutoMapper;
using Euphorolog.Database.Context;
using Euphorolog.Database.Models;
using Euphorolog.Repository.Contracts;
using Euphorolog.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.Services
{
    public class StoriesService : IStoriesService
    {
        public readonly EuphorologContext _context;
        public readonly IStoriesRepository _storiesRepository;
        public readonly IMapper _mapper;
        public StoriesService(EuphorologContext context, IStoriesRepository storiesRepository, IMapper mapper)
        {
            _context = context;
            _storiesRepository = storiesRepository;
            _mapper = mapper;
        }
        public async Task<List<Stories>> GetAllStoriesAsync()
        {
            var ret = await _storiesRepository.GetAllStoriesAsync();
            return ret;

        }
        public async Task<StoriesDTO> GetStoryByIdAsync(string id)
        {
            var ret = await _storiesRepository.GetStoryByIdAsync(id);
            StoriesDTO story = _mapper.Map<StoriesDTO>(ret);
            return story;

        }
        public async Task<List<Stories>> PostStoryAsync(Stories story)
        {
            int mx = 0;
            mx = await _context.stories.MaxAsync(s => s.authorName == story.authorName ? s.storyNo : 0);
            story.storyId = $"{story.authorName}-{(mx + 1)}";
            story.storyNo = mx + 1;
            int openingLineLength = Math.Min(99, Math.Max(story.storyDescription.Length-1,0));
            story.openingLines = story.storyDescription.Substring(0,openingLineLength) + "...";
            story.createdAt = DateTime.UtcNow;
            story.updatedAt = DateTime.UtcNow;
            return await _storiesRepository.PostStoryAsync(story);
        }
        public async Task<List<Stories>> DeleteStoryAsync(string id)
        {
            return await _storiesRepository.DeleteStoryAsync(id);
        }
        public async Task<Stories> UpdateStoryAsync(string id, Stories story)
        {
            return await _storiesRepository.UpdateStoryAsync(id, story);
        }
    }
}
