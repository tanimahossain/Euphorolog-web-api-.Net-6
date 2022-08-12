using Euphorolog.Database.Context;
using Euphorolog.Database.Models;
using Euphorolog.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Repository.Repositories
{
    public class UsersRepository : IUsersRepository
    {

        public readonly EuphorologContext _context;
        public UsersRepository(EuphorologContext context)
        {
            _context = context;
        }
        public async Task<bool> UserExists(string userName)
        {
            if(await _context.users.AnyAsync(u => u.userName.ToLower() == userName.ToLower()))
            {
                return true;
            }
            return false;
        }
        public async Task<DateTime> GetPasswordChangedAtAsync(string id)
        {
            var ret = await _context.users.FirstOrDefaultAsync(s => s.userName == id);
            if (ret.passChangedAt == null)
            {
               return DateTime.UtcNow;
            }
            return (DateTime)ret.passChangedAt;
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _context.users.ToListAsync();

        }
        public async Task<Users?> GetUserByIdAsync(string id)
        {
            return await _context.users.FirstOrDefaultAsync(s => s.userName == id);

        }
        public async Task<Users> SignUp(Users user)
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<List<Users>> CreateUserAsync(Users user)
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            var ret = await _context.users.ToListAsync();
            return ret;
        }
        public async Task<List<Users>> DeleteUserAsync(string id)
        {
            var ret = await _context.users.FirstOrDefaultAsync(s => s.userName == id);
            if (ret != null)
            {
                _context.users.Remove(ret);
            }
            await _context.SaveChangesAsync();
            return await _context.users.ToListAsync();
        }
        public async Task<Users> UpdateUserAsync(string id, Users user)
        {
            var ret = await _context.users.FirstOrDefaultAsync(s => s.userName == id);
            if (user == null)
            {
                return ret;
            }
            if (user.fullName != null)
            {
                ret.fullName = user.fullName;
            }
            if (user.password != null)
            {
                ret.password = user.password;
                ret.passChangedAt = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
            return ret;
        }
    }
}
