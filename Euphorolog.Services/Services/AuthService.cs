using Euphorolog.Database.Context;
using Euphorolog.Database.Models;
using Euphorolog.Repository.Contracts;
using Euphorolog.Services.Contracts;
using System.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Euphorolog.Services.Utils;
using System.Runtime.Serialization;
using Euphorolog.Services.DTOs;
using Microsoft.Extensions.Configuration;

namespace Euphorolog.Services.Services
{
    public class AuthService : IAuthService
    {
        public readonly EuphorologContext _context;
        public readonly IUsersRepository _usersRepository;
        public readonly IConfiguration _configuration;
        public AuthService(EuphorologContext context, IUsersRepository usersRepository,IConfiguration configuration)
        {
            _context = context;
            _usersRepository = usersRepository;
            _configuration = configuration;
        }

        public async Task<Users> SignUp(Users user)
        {
            if (await _usersRepository.UserExists(user.userName))
            {
                throw new Exception("User already exists!");
            }
            var _utils = new Utilities(_configuration);
            _utils.CreatePasswordHash(user.password, out byte[] passwordHash, out byte[] passwordSalt);
            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            user.passChangedAt = DateTime.UtcNow;
            user.passChangedflag = true;
            return await _usersRepository.SignUp(user);
        }

        public async Task<LogInOutputDTO> LogIn(LogInInputDTO user)
        {
            var userInfo = await _usersRepository.GetUserByIdAsync(user.userName);
            if(userInfo == null)
            {
                throw new Exception("User doesn't exist!");
            }
            var _utils = new Utilities(_configuration);
            if(!_utils.VerifyPasswordHash(user.password, userInfo.passwordHash, userInfo.passwordSalt))
            {
                throw new Exception("username or password wrong!");
            }
            var sendInfo = new LogInOutputDTO();
            sendInfo.userName = user.userName;
            sendInfo.token = _utils.CreateJWTToken(userInfo);
            return sendInfo;
        }
    }
}
