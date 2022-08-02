using Euphorolog.Database.Models;
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
        Task<List<Users>> GetAllUsersAsync();
        Task<Users> GetUserByIdAsync(string id);
        Task<List<Users>> CreateUserAsync(Users user);
        Task<List<Users>> DeleteUserAsync(string id);
        Task<Users> UpdateUserAsync(string id, Users user);
        //Users SignUp(Users user);
        //Users LogIn(string username, string password);
    }
}
