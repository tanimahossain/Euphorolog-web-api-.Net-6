using Euphorolog.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Repository.Contracts
{
    public interface IStoriesRepository
    {
        Task<List<Stories>> GetAllStoriesAsync();

        Task<Stories> GetStoryByIdAsync(string id);
        Task<List<Stories>> PostStoryAsync(Stories story);
        Task<List<Stories>> DeleteStoryAsync(string id);
        Task<Stories> UpdateStoryAsync(string id, Stories story);
    }
}
