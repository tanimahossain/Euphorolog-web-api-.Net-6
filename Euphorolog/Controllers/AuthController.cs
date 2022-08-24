using Euphorolog.Database.Models;
using Euphorolog.Services.Contracts;
using Euphorolog.Services.DTOs.AuthDTOs;
using Euphorolog.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Euphorolog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("SignUp")]
        public async Task<ActionResult<AuthResponse<SignUpResponseDTO>>> SignUpAsync(SignUpRequestDTO user)
        {
            var response = await _authService.SignUpAsync(user);
            return Ok(new AuthResponse<SignUpResponseDTO>(response));
        }
        [HttpPost("LogIn")]
        public async Task<ActionResult<AuthResponse<LogInResponseDTO>>> LogInAsync(LogInRequestDTO user)
        {
            var response = await _authService.LogInAsync(user);
            return Ok(new AuthResponse<LogInResponseDTO>(response));
        }
        [HttpPost("verify"),Authorize]
        public async Task<ActionResult<AuthResponse<LogInResponseDTO>>> VerifyAsync()
        {
            var response = await _authService.VerifyAsync();
            return Ok(new AuthResponse<LogInResponseDTO>(response));
        }
    }
}
