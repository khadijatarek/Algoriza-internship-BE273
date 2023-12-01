using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDoctorController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AdminDoctorController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctor(DoctorRegisterDTO registerDTO)
        {
            var result = await _authService.RegisterDoctorAsync(registerDTO);

            if (result.Succeeded)
            {
                return Ok(result); // Return Ok without specifying a result object
            }

            // Handle specific errors or return the entire result object
            if (result.Errors.Any(error => error.Code == "SomeSpecificErrorCode"))
            {
                return BadRequest("Custom error message for a specific condition");
            }

            return BadRequest(result.Errors); // Return the entire result object
        }

    }
}
