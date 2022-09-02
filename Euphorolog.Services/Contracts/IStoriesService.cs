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
        Task<List<GetStoryResponseDTO>> GetAllStoriesAsync(int pageNumber, int pageSize);
        Task<GetStoryResponseDTO> GetStoryByIdAsync(string id);
        Task<List<GetStoryResponseDTO>> GetStoriesByUserIdAsync(int pageNumber, int pageSize, string username);
        Task<int> GetTotalStoryCountOfAUserAsync(string username);
        Task<GetStoryResponseDTO> PostStoryAsync(PostStoryRequestDTO story);
        Task<bool> DeleteStoryAsync(string id);
        Task<GetStoryResponseDTO> UpdateStoryAsync(string id, UpdateStoryRequestDTO story);
    }
}
