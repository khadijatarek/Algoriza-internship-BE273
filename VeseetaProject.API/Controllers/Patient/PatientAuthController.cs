using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers.Patient
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientAuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;

        public PatientAuthController(UserManager<ApplicationUser>userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [HttpPost("PatientRegister")]
        public async Task<IActionResult> RegisterPatient([FromForm] PatientRegisterDTO userDTO)
        {
            var result = await _authService.RegisterPatientAsync(userDTO);
            if(ModelState.IsValid)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        //NOT NEEDED HERE
        [HttpGet("GetAllPatients")]
        public IActionResult GetAllPatients()
        {
            return  Ok( _userManager.Users.Select(u => u.Email));
        }
    }
}
