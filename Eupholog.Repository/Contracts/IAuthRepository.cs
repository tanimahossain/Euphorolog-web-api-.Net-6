using Euphorolog.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Repository.Contracts
{
    public interface IAuthRepository
    {
        Task<Users> SignUp(Users user);
        Task<Users> LogIn(Users user);
    }
}
