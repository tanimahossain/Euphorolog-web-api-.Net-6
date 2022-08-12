﻿using Euphorolog.Database.Context;
using Euphorolog.Database.Models;
using Euphorolog.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Repository.Repositories
{
    public class StoriesRepository : IStoriesRepository
    {
        public readonly EuphorologContext _context;
        public StoriesRepository(EuphorologContext context)
        {
            _context = context;
        }
        public async Task<int> TotalStoryNoAsync()
        {
            var ret = await _context.stories.CountAsync();
            return ret;
        }

        public async Task<List<Stories>> GetAllStoriesAsync(int pageNumber, int pageSize)
        {
            var ret = await _context.stories
                .OrderByDescending(s=>s.createdAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return ret;

        }
        public async Task<Stories?> GetStoryByIdAsync(string id)
        {
            var ret = await _context.stories.FirstOrDefaultAsync(s => s.storyId == id);
            return ret;

        }

        public async Task<int> MaxStoryNoByUserId(string id)
        {
            int? mx = await _context.stories.MaxAsync(s => s.authorName == id ? s.storyNo : 0);
            if (mx == null)
                mx = 0;
            return (int)mx;
        }
        public async Task<Stories> PostStoryAsync(Stories story)
        {
            _context.stories.Add(story);
            await _context.SaveChangesAsync();
            return story;
        }
        public async Task<List<Stories>> DeleteStoryAsync(string id)
        {
            var ret = await _context.stories.FirstOrDefaultAsync(s => s.storyId == id);
            if(ret != null)
                _context.stories.Remove(ret);
            await _context.SaveChangesAsync();
            return await _context.stories.OrderByDescending(s => s.createdAt).ToListAsync();
        }
        public async Task<Stories> UpdateStoryAsync(string id, Stories story)
        {
            var ret = await _context.stories.FirstOrDefaultAsync(s => s.storyId == id);
            if(story == null)
            {
                return ret;
            }
            if (story.storyTitle != null)
            {
                ret.storyTitle = story.storyTitle;
                ret.updatedAt = DateTime.UtcNow;
            }
            if (story.storyDescription != null)
            {
                ret.storyDescription = story.storyDescription;
                ret.openingLines = story.openingLines;
                ret.updatedAt = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
            return ret;
        }
    }
}
