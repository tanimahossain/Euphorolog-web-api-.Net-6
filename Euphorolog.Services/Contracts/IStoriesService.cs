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
        Task<List<GetAllStoriesResponseDTO>> GetAllStoriesAsync(int pageNumber, int pageSize);
        Task<GetStoryByIdResponseDTO> GetStoryByIdAsync(string id);
        Task<GetStoryByIdResponseDTO> PostStoryAsync(PostStoryRequestDTO story);
        Task<List<GetAllStoriesResponseDTO>> DeleteStoryAsync(string id);
        Task<GetStoryByIdResponseDTO> UpdateStoryAsync(string id, UpdateStoryRequestDTO story);
        Task<int> TotalStoryNoAsync();
    }
}
