using Euphorolog.Database.Models;
using Euphorolog.Services.Contracts;
using Euphorolog.Services.DTOs.AuthDTOs;
using Euphorolog.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Euphorolog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("SignUp")]
        public async Task<ActionResult<Response<SignUpResponseDTO>>> SignUp(SignUpRequestDTO user)
        {
            var response = await _authService.SignUp(user);
            return Ok(new Response<SignUpResponseDTO>(response));
        }
        [HttpPost("LogIn")]
        public async Task<ActionResult<Response<LogInResponseDTO>>> LogIn(LogInRequestDTO user)
        {
            var response = await _authService.LogIn(user);
            //var presponse = new Response<LogInResponseDTO>(response);
            //return Ok(p);
            return Ok(new Response<LogInResponseDTO>(response));
        }
    }
}
