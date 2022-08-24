using AutoMapper;
using Euphorolog.Database.Models;
using Euphorolog.Repository.Contracts;
using Euphorolog.Services.Contracts;
using Euphorolog.Services.CustomExceptions;
using Euphorolog.Services.DTOs.UsersDTOs;
using Euphorolog.Services.DTOValidators;
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
        private readonly IUsersRepository _usersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUtilities _utils;
        private readonly IMapper _mapper;
        private readonly MainDTOValidator<UpdateUserInfoRequestDTO> _updateUserInfoValidator;
        public UsersService(
            IUsersRepository usersRepository,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IUtilities utils,
            IMapper mapper,
            MainDTOValidator<UpdateUserInfoRequestDTO> updateUserInfoValidator)
        {
            _usersRepository = usersRepository;
            _httpContextAccessor = httpContextAccessor;
            _utils = utils;
            _mapper = mapper;
            _updateUserInfoValidator = updateUserInfoValidator;
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
        public async Task DeleteUserAsync(string id)
        {
            /*
             * If the token is valid based on time
             * If the user Exists
             * If the id is their's
             */

            // token validation based on time
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
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
            return;
        }
        public async Task<UpdateUserInfoResponseDTO> UpdateUserAsync(string id, UpdateUserInfoRequestDTO req)
        {
            /*
             * If the token is valid based on time
             * If the user Exists
             * If the id is their's
             */

            _updateUserInfoValidator.ValidateDTO(req);
            // token validation based on time
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
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
            bool passChangedflag = false;
            if (req.password != null)
            {
                _utils.CreatePasswordHash(req.password, out byte[] passwordHash, out byte[] passwordSalt);
                reqUser.passwordHash = passwordHash;
                reqUser.passwordSalt = passwordSalt;
                passChangedflag = true;
            }
            var updatedUser = await _usersRepository.UpdateUserAsync(id, reqUser);
            var ret = _mapper.Map<UpdateUserInfoResponseDTO>(updatedUser);
            if (passChangedflag)
            {
                ret.token = _utils.CreateJWTToken(updatedUser);
            }
            return ret;
        }
    }
}
