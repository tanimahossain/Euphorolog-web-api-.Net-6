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
        Task<SignUpResponseDTO> SignUp(SignUpRequestDTO user);
        Task<LogInResponseDTO> LogIn(LogInRequestDTO user);
        Task<LogInResponseDTO> Verify();
    }
}
