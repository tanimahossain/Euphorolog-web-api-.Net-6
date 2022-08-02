//using Euphorolog.Database.Models;
using Euphorolog.Database.Models;
using Euphorolog.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.Services
{
    public interface IStoriesService
    {
        Task<List<Stories>> GetAllStoriesAsync();
        Task<StoriesDTO> GetStoryByIdAsync(string id);
        Task<List<Stories>> PostStoryAsync(Stories story);
        Task<List<Stories>> DeleteStoryAsync(string id);
        Task<Stories> UpdateStoryAsync(string id, Stories story);
    }
}
