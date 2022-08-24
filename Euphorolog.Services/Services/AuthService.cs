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
using Microsoft.Extensions.Configuration;
using Euphorolog.Services.DTOs.AuthDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Euphorolog.Services.CustomExceptions;
using FluentValidation;
using Euphorolog.Services.DTOValidators;
using System.Security.Claims;
using Microsoft.Net.Http.Headers;

namespace Euphorolog.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly EuphorologContext _context;
        private readonly IUsersRepository _usersRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly MainDTOValidator<LogInRequestDTO> _logInValidator;
        private readonly MainDTOValidator<SignUpRequestDTO> _signUpValidator;
        private readonly IUtilities _utils;
        public AuthService(
            EuphorologContext context,
            IUsersRepository usersRepository,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUtilities utils,
            MainDTOValidator<LogInRequestDTO> logInValidator,
            MainDTOValidator<SignUpRequestDTO> signUpValidator
        )
        {
            _context = context;
            _usersRepository = usersRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _utils = utils;
            _logInValidator = logInValidator;
            _signUpValidator = signUpValidator;
        }

        public async Task<SignUpResponseDTO> SignUpAsync(SignUpRequestDTO req)
        {
            _signUpValidator.ValidateDTO(req);
            if (await _usersRepository.CheckUserExistsAsync(req.userName))
            {
                throw new BadRequestException("User already exists!");
            }
            if (await _usersRepository.CheckEmailUsedAsync(req.eMail))
            {
                throw new BadRequestException("Email already used!");
            }
            var user = _mapper.Map<Users>(req);
            _utils.CreatePasswordHash(req.password, out byte[] passwordHash, out byte[] passwordSalt);
            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            user.passChangedAt = DateTime.UtcNow;
            var userInfo = await _usersRepository.SignUpAsync(user);
            var ret = _mapper.Map<SignUpResponseDTO>(userInfo);
            ret.token = _utils.CreateJWTToken(userInfo);
            return ret;
        }

        public async Task<LogInResponseDTO> LogInAsync(LogInRequestDTO user)
        {
            _logInValidator.ValidateDTO(user);
            var userInfo = await _usersRepository.GetUserByIdAsync(user.userName);
            if (userInfo == null)
            {
                throw new NotFoundException("User doesn't exist!");
            }
            if (!_utils.VerifyPasswordHash(user.password, userInfo.passwordHash, userInfo.passwordSalt))
            {
                throw new BadRequestException("username or password wrong!");
            }
            var sendInfo = new LogInResponseDTO();
            sendInfo.userName = user.userName;
            sendInfo.token = _utils.CreateJWTToken(userInfo);
            return sendInfo;
        }

        public async Task<LogInResponseDTO> VerifyAsync()
        {
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null)
            {
                throw new UnAuthorizedException("Log In First!");
            }
            var userInfo = await _usersRepository.GetUserByIdAsync(username);
            if (userInfo == null)
            {
                throw new NotFoundException("User doesn't exist!");
            }
            if (!_utils.tokenStillValid(await _usersRepository.GetPasswordChangedAtAsync(username)))
            {
                throw new UnAuthorizedException("LogIn First!");
            }
            var sendInfo = new LogInResponseDTO();
            sendInfo.userName = username;
            sendInfo.token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("bearer ", "");
            sendInfo.token = sendInfo.token.ToString().Replace("Bearer ", "");
            return sendInfo;
        }
    }
}
