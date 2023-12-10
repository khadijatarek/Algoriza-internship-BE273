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
    [Authorize(Roles = "Admin")]

    public class AdminDoctorController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IDoctorService _doctorService;
        private readonly IImageService _imageService;

        public AdminDoctorController(IAuthService authService, IDoctorService DoctorService, IImageService imageService)
        {
            _authService = authService;
            _doctorService = DoctorService;
            _imageService = imageService;
        }

        [HttpGet("Get All Doctors")]
        public async Task<IActionResult> getAllDoctors(int? pageNum, int? pageSize)
        {
            return Ok(await _doctorService.GetAllDoctors(pageNum,pageSize));
        }

        [HttpGet("GetDoctorById")]
        public async Task<IActionResult> getDoctor(int id)
        {
            return await _doctorService.GetDoctorById(id);
        }

        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromForm] DoctorRegisterDTO registerDTO)
        {
            var ImageUrl = _imageService.SaveImageToFolder(registerDTO.Image, registerDTO.Email);
            
            var result = await _authService.RegisterDoctorAsync(registerDTO, ImageUrl:ImageUrl);

            if (result.Succeeded)
            {
                return Ok(result); // Return Ok without specifying a result object
            }

            if (result.Errors.Any(error => error.Code == " error"))
            {
                return BadRequest("Custom error message for a specific condition");
            }

            return BadRequest(result.Errors); // Return the entire result object
        }

        [HttpPut("EditDoctor/{id}")]
        public async Task<IActionResult> Update([FromForm]DoctorRegisterDTO doctorDTO,int id)
        {
            if (ModelState.IsValid)
            {
                var ImageUrl = _imageService.SaveImageToFolder(doctorDTO.Image, doctorDTO.Email);
                var result = await _doctorService.UpdateDoctor(doctorDTO, id, ImageUrl);
                return result;
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteDoctor")]
        public async Task<IActionResult> Delete(int doctorId)
        {
            return await _doctorService.DeleteDoctor(doctorId);
        }
    }
}
