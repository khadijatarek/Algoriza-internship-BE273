using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers.Doctor
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        //[HttpGet("GET DOCTOR BY ID")]
        //public async Task<IActionResult> getbyId(int id)
        //{
        //    return Ok(await _doctorService.GetDoctor_ById(id));
        //}


        [HttpPost("api/[controller]/Login")]
        public IActionResult login(string username, string password)
        {
            throw new NotImplementedException();
        }

        [HttpPost("api/[controller]/ConfirmCheckup")]
        public async Task<IActionResult> confirmCheckup(int bookingId)
        {
            var result = await _doctorService.ConfirmCheckUpAsync(bookingId);
            return Ok(result);
        }

        [HttpGet("api/[controller]/Booking/[action]")]
        public async Task<IActionResult> GetAll()
        {

            return Ok(await _doctorService.GetAllDoctors());
        }



        [HttpPost("api/[controller]/Appointments/[action]")]
        public async Task<IActionResult> Add(AppointmentDTO appointmentDTO)
        {
            if (ModelState.IsValid)
            {
                // Retrieve DoctorId from the JWT token
                var doctorIdClaim = HttpContext.User.FindFirst("DoctorId");

                if (!int.TryParse(doctorIdClaim.Value, out var doctorId))
                {
                    return BadRequest("Invalid or missing DoctorId in the token.");
                }

                return await _doctorService.AddAppointment(doctorId, appointmentDTO);
            }
            else { 
                return BadRequest(ModelState); 
            }
        }
        
        [HttpPut("api/[controller]/Appointments/[action]")]
        public IActionResult Update(int doctorId,AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete("api/[controller]/Appointments/[action]")]
        public IActionResult Delete(int doctorId)
        {
            throw new NotImplementedException();
        }


       
    }
   
}
