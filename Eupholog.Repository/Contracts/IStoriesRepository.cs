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

        Task<Stories?> GetStoryByIdAsync(string id);
        Task<int>MaxStoryNoByUserId(string id);
        Task<Stories> PostStoryAsync(Stories story);
        Task<List<Stories>> DeleteStoryAsync(string id);
        Task<Stories> UpdateStoryAsync(string id, Stories story);
        Task<int> TotalStoryNoAsync();
    }
}
