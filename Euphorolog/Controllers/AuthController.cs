using Euphorolog.Database.Models;
using Euphorolog.Services.Contracts;
using Euphorolog.Services.DTOs;
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
        public async Task<ActionResult<Users>> SignUp(Users user)
        {
            return Ok(await _authService.SignUp(user));
        }
        [HttpPost("LogIn")]
        public async Task<ActionResult<LogInOutputDTO>> LogIn(LogInInputDTO user)
        {
            return Ok(await _authService.LogIn(user));
        }
    }
}
