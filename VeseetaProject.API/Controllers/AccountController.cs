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
        private readonly IImageService _imageService;

        public AccountController(IAuthService authService, IImageService imageService)
        {
            _authService = authService;
            _imageService = imageService;
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
                if (registerDTO.Image != null)
                {
                    var imageUrl = _imageService.SaveImageToFolder(registerDTO.Image, registerDTO.Email);
                    var result = await _authService.Registeration(registerDTO, imageUrl);
                    return result;
                }
                else
                {
                    var result = await _authService.Registeration(registerDTO);

                    return result;
                }
                
            }
        }
       
    }
}
