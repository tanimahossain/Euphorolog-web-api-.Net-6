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
        Task<List<Stories>> GetAllStoriesAsync(int pageNumber, int pageSize);
        Task<List<Stories>> GetStoriesByUserIdAsync(int pageNumber, int pageSize, string username);
        Task<int> GetTotalStoryCountOfAUserAsync(string username);

        Task<Stories?> GetStoryByIdAsync(string id);
        Task<int?> GetMaxStoryNoByUserIdAsync(string id);
        Task<Stories> PostStoryAsync(Stories story);
        Task<bool> DeleteStoryAsync(string id);
        Task<Stories> UpdateStoryAsync(string id, Stories story);
        Task<int> GetTotalStoryCountAsync();
    }
}
