//using Euphorolog.Database.Models;
using Euphorolog.Database.Models;
using Euphorolog.Services.DTOs.StoriesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.Services
{
    public interface IStoriesService
    {
        Task<int> GetTotalStoryCountAsync();
        Task<List<GetAllStoriesResponseDTO>> GetAllStoriesAsync(int pageNumber, int pageSize);
        Task<GetStoryByIdResponseDTO> GetStoryByIdAsync(string id);
        Task<List<GetAllStoriesResponseDTO>> GetStoriesByUserIdAsync(int pageNumber, int pageSize, string username);
        Task<int> GetTotalStoryCountOfAUserAsync(string username);
        Task<GetStoryByIdResponseDTO> PostStoryAsync(PostStoryRequestDTO story);
        Task DeleteStoryAsync(string id);
        Task<GetStoryByIdResponseDTO> UpdateStoryAsync(string id, UpdateStoryRequestDTO story);
    }
}
