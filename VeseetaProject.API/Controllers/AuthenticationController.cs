using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }

            var result = await _authService.LoginAsync(loginDTO);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Login successful" });
            }

            return BadRequest(new { Message = "Invalid login attempt", Errors = result.Errors.Select(e => e.Description) });
        }
    }
}
