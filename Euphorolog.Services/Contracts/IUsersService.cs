using Euphorolog.Database.Models;
using Euphorolog.Services.DTOs.StoriesDTOs;
using Euphorolog.Services.DTOs.UsersDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Euphorolog.Database.Models;

namespace Euphorolog.Services.Services
{
    public interface IUsersService
    {
        Task<List<UserInfoResponseDTO>> GetAllUsersAsync();
        Task<UserInfoResponseDTO> GetUserByIdAsync(string id);
        Task DeleteUserAsync(string id);
        Task<UpdateUserInfoResponseDTO> UpdateUserAsync(string id, UpdateUserInfoRequestDTO user);
    }
}
