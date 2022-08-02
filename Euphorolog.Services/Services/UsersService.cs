using Euphorolog.Database.Context;
using Euphorolog.Database.Models;
using Euphorolog.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.Services
{
    public class UsersService : IUsersService
    {
        public readonly EuphorologContext _context;
        public readonly IUsersRepository _usersRepository;
        public UsersService(EuphorologContext context, IUsersRepository usersRepository)
        {
            _context = context;
            _usersRepository = usersRepository;
        }
        public async Task<List<Users>> GetAllUsersAsync()
        {
            var ret = await _usersRepository.GetAllUsersAsync();
            return ret;

        }
        public async Task<Users> GetUserByIdAsync(string id)
        {
            var ret = await _usersRepository.GetUserByIdAsync(id);
            return ret;

        }
        public async Task<List<Users>> CreateUserAsync(Users user)
        {
            user.passChangedAt = DateTime.UtcNow;
            user.passChangedflag = true;
            return await _usersRepository.CreateUserAsync(user);
        }
        public async Task<List<Users>> DeleteUserAsync(string id)
        {
            return await _usersRepository.DeleteUserAsync(id);
        }
        public async Task<Users> UpdateUserAsync(string id, Users user)
        {
            return await _usersRepository.UpdateUserAsync(id, user);
        }
    }
}
