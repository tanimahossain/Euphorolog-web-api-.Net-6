using Euphorolog.Database.Context;
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

        public async Task<List<Stories>> GetAllStoriesAsync()
        {
            var ret = await _context.stories.ToListAsync();
            return ret;

        }
        public async Task<Stories> GetStoryByIdAsync(string id)
        {
            var ret = await _context.stories.FirstOrDefaultAsync(s => s.storyId == id);
            if (ret == null){
                throw new FileNotFoundException(@"No Such Story!");
            }
            return ret;

        }
        public async Task<List<Stories>> PostStoryAsync(Stories story)
        {
            try
            {
                _context.stories.Add(story);
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw err;
            }
            var ret = await _context.stories.ToListAsync();
            return ret;
        }
        public async Task<List<Stories>> DeleteStoryAsync(string id)
        {
            var ret = await _context.stories.FirstOrDefaultAsync(s => s.storyId == id);
            if (ret == null)
            {
                throw new FileNotFoundException(@"No Such Story!");
            }
            _context.stories.Remove(ret);
            await _context.SaveChangesAsync();
            return await _context.stories.ToListAsync();
        }
        public async Task<Stories> UpdateStoryAsync(string id, Stories story)
        {
            var ret = await _context.stories.FirstOrDefaultAsync(s => s.storyId == id);
            if (ret == null)
            {
                throw new FileNotFoundException(@"No Such Story!");
            }
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
                ret.updatedAt = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
            return ret;
        }
    }
}
