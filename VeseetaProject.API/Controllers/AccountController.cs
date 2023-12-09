using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }

            var result = await _authService.Login(loginDTO);

            return result;
            
        }
        [HttpPost("Patient Registeration")]
        public async Task<IActionResult> Register([FromForm] RegisterationDTO registerDTO)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _authService.Registeration(registerDTO);
                return result;
            }
        }
       
    }
}
