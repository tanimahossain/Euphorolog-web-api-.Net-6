using Euphorolog.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Repository.Contracts
{
    public interface IUsersRepository
    {
        Task<List<Users>> GetAllUsersAsync();
        Task<Users?> GetUserByIdAsync(string id);
        Task<Users> SignUp(Users user);
        Task<List<Users>> CreateUserAsync(Users user);
        Task DeleteUserAsync(string id);
        Task<Users> UpdateUserAsync(string id, Users user);
        Task<bool> UserExists(string id);
        Task<bool> EmailUsed(string id);
        Task<DateTime> GetPasswordChangedAtAsync(string id);
    }
}
