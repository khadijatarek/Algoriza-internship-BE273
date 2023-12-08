using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Services;
using VeseetaProject.Services;

namespace VeseetaProject.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]

    public class AdminDoctorController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IDoctorService _doctorService;

        public AdminDoctorController(IAuthService authService, IDoctorService DoctorService)
        {
            _authService = authService;
            _doctorService = DoctorService;
        }
        [HttpGet("Get All Doctors")]
        public async Task<IActionResult> getAllDoctors()
        {
            return Ok(await _doctorService.GetAllDoctors());
        }


        [HttpGet("Get Doctor By Id")]
        public async Task<IActionResult> getDoctor([FromRoute] int id)
        {
            throw new NotImplementedException();
            //return Ok(await _doctorService.GetDoctorById(id));
        }


        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromForm]DoctorRegisterDTO registerDTO)
        {
            var result = await _authService.RegisterDoctorAsync(registerDTO);

            if (result.Succeeded)
            {
                return Ok(result); // Return Ok without specifying a result object
            }

            // Handle specific errors or return the entire result object
            if (result.Errors.Any(error => error.Code == " error"))
            {
                return BadRequest("Custom error message for a specific condition");
            }

            return BadRequest(result.Errors); // Return the entire result object
        }

        [HttpPut("EditDoctor")]
        public ActionResult Update(DoctorDetailsDTO doctor)
        {
            throw new NotImplementedException();
        }
        [HttpDelete("DeleteDoctor")]
        public ActionResult Update(int doctorId)
        {
            throw new NotImplementedException();
        }
    }
}
