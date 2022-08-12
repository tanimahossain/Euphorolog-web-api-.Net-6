using AutoMapper;
using Euphorolog.Database.Context;
using Euphorolog.Database.Models;
using Euphorolog.Repository.Contracts;
using Euphorolog.Services.CustomExceptions;
using Euphorolog.Services.DTOs.UsersDTOs;
using Euphorolog.Services.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.Services
{
    public class UsersService : IUsersService
    {
        public readonly EuphorologContext _context;
        public readonly IUsersRepository _usersRepository;
        public readonly IConfiguration _configuration;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IMapper _mapper;
        public UsersService(
            EuphorologContext context,
            IUsersRepository usersRepository,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _context = context;
            _usersRepository = usersRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<List<UserInfoResponseDTO>> GetAllUsersAsync()
        {
            var ret = await _usersRepository.GetAllUsersAsync();
            return _mapper.Map<List<UserInfoResponseDTO>>(ret);

        }
        public async Task<UserInfoResponseDTO> GetUserByIdAsync(string id)
        {
            var ret = await _usersRepository.GetUserByIdAsync(id);
            if(ret == null)
            {
                throw new NotFoundException("User doesn't Exist");
            }
            return _mapper.Map<UserInfoResponseDTO>(ret);

        }
        public async Task<List<UserInfoResponseDTO>> DeleteUserAsync(string id)
        {
            /*
             * If the user is logged in
             * If the token is valid based on time
             * If the user Exists
             * If the id is their's
             */

            // User logged In
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            if(username == null)
            {
                throw new UnAuthorizedException("Log In First!");
            }

            // token validation based on time
            var _utils = new Utilities(_configuration, _httpContextAccessor);
            if (!_utils.tokenStillValid(await _usersRepository.GetPasswordChangedAtAsync(id)))
            {
                throw new UnAuthorizedException("LogIn First!");
            }

            // user exists
            var user = await _usersRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User doesn't Exist!");
            }

            //their id
            if (!String.Equals(username, id))
            {
                throw new ForbiddenException("Can't Delete someone else's id");
            }


            await _usersRepository.DeleteUserAsync(id);
            var ret = await _usersRepository.GetAllUsersAsync();
            return _mapper.Map<List<UserInfoResponseDTO>>(ret);
        }
        public async Task<UpdateUserInfoResponseDTO> UpdateUserAsync(string id, UpdateUserInfoRequestDTO req)
        {

            /*
             * If the user is logged in
             * If the token is valid based on time
             * If the user Exists
             * If the id is their's
             */

            // User logged In
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null)
            {
                throw new UnAuthorizedException("Log In First!");
            }

            // token validation based on time
            var _utils = new Utilities(_configuration, _httpContextAccessor);
            if (!_utils.tokenStillValid(await _usersRepository.GetPasswordChangedAtAsync(id)))
            {
                throw new UnAuthorizedException("LogIn First!");
            }

            // user exists
            var user = await _usersRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User doesn't Exist!");
            }

            //their id
            if (!String.Equals(username, id))
            {
                throw new ForbiddenException("Can't Delete someone else's id");
            }


            var reqUser = _mapper.Map<Users>(req);
            var updatedUser = await _usersRepository.UpdateUserAsync(id, reqUser);
            bool passChangedflag = false;
            if (reqUser.password != null)
            {
                passChangedflag = true;
            }
            var ret = _mapper.Map<UpdateUserInfoResponseDTO>(updatedUser);
            if (passChangedflag)
            {
                ret.token = _utils.CreateJWTToken(updatedUser);
            }
            return ret;
        }
    }
}
