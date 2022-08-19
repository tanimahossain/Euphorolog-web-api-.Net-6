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
        public readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("SignUp")]
        public async Task<ActionResult<AuthResponse<SignUpResponseDTO>>> SignUp(SignUpRequestDTO user)
        {
            var response = await _authService.SignUp(user);
            return Ok(new AuthResponse<SignUpResponseDTO>(response));
        }
        [HttpPost("LogIn")]
        public async Task<ActionResult<AuthResponse<LogInResponseDTO>>> LogIn(LogInRequestDTO user)
        {
            var response = await _authService.LogIn(user);
            return Ok(new AuthResponse<LogInResponseDTO>(response));
        }
        [HttpPost("verify"),Authorize]
        public async Task<ActionResult<AuthResponse<LogInResponseDTO>>> Verify()
        {
            var response = await _authService.Verify();
            return Ok(new AuthResponse<LogInResponseDTO>(response));
        }
    }
}
