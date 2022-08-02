using Euphorolog.Database.Models;
using Euphorolog.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.Contracts
{
    public interface IAuthService
    {
        Task<Users> SignUp(Users user);
        Task<LogInOutputDTO> LogIn(LogInInputDTO user);
    }
}
