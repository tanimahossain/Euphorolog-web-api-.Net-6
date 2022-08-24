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
        Task<Users> SignUpAsync(Users user);
        Task<List<Users>> CreateUserAsync(Users user);
        Task DeleteUserAsync(string id);
        Task<Users> UpdateUserAsync(string id, Users user);
        Task<bool> CheckUserExistsAsync(string id);
        Task<bool> CheckEmailUsedAsync(string id);
        Task<DateTime> GetPasswordChangedAtAsync(string id);
    }
}
