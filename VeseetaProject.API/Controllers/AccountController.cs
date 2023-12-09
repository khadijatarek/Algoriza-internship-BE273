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
            //if (result.Succeeded)
            //{
            //    return Ok(new { Message = "Login successful" });
            //}

            //return BadRequest(new { Message = "Invalid login attempt", Errors = result.Errors.Select(e => e.Description) });
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
        //[HttpPost("Login")]
        //public async Task<IActionResult> Login([FromForm] LoginDTO loginDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _authService.Registeration(loginDTO);
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}
    }
}
