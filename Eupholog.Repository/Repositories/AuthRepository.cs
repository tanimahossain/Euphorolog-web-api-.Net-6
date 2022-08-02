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
    public class AuthRepository : IAuthRepository
    {

        public readonly EuphorologContext _context;
        public AuthRepository(EuphorologContext context)
        {
            _context = context;
        }

        public async Task<Users> SignUp(Users user)
        {
            try
            {
                _context.users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw err;
            }
            return user;
        }

        public async Task<Users> LogIn(Users user)
        {
            try
            {
                _context.users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw err;
            }
            return user;
        }
    }
}
