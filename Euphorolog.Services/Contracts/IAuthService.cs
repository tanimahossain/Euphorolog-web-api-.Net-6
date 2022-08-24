using Euphorolog.Database.Models;
using Euphorolog.Services.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.Contracts
{
    public interface IAuthService
    {
        Task<SignUpResponseDTO> SignUpAsync(SignUpRequestDTO user);
        Task<LogInResponseDTO> LogInAsync(LogInRequestDTO user);
        Task<LogInResponseDTO> VerifyAsync();
    }
}
