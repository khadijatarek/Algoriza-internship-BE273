using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Services;
using VeseetaProject.Services;

namespace VeseetaProject.API.Controllers.Doctor
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IBookingService _bookingService;


        public DoctorController(IDoctorService doctorService, IBookingService bookingService)
        {
            _doctorService = doctorService;
            _bookingService = bookingService;
        }


        [HttpPost("api/[controller]/ConfirmCheckup")]
        public async Task<IActionResult> confirmCheckup(int bookingId)
        {
            var doctorIdClaim = HttpContext.User.FindFirst("DoctorId");

            if (!int.TryParse(doctorIdClaim.Value, out var doctorId))
            {
                return BadRequest("Invalid or missing DoctorId in the token.");
            }

            var result = await _doctorService.ConfirmCheckUpAsync(doctorId,bookingId);
            return result;
        }



        [HttpGet("api/[controller]/Booking/[action]")]
        public async Task<IActionResult> GetAll(int? pageNum, int?PageSize, string? search)
        {
            var doctorIdClaim = HttpContext.User.FindFirst("DoctorId");
            if (!int.TryParse(doctorIdClaim.Value, out var doctorId))
            {
                return BadRequest("Invalid or missing DoctorId in the token.");
            }
            return await _bookingService.GetAllDoctorBookings(doctorId,pageNum,PageSize,search); ;
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
        public async Task<IActionResult> UpdateAppointment(AppointmentDTO appointmentDTO)
        {
            var doctorIdClaim = HttpContext.User.FindFirst("DoctorId");

            if (!int.TryParse(doctorIdClaim.Value, out var doctorId))
            {
                return BadRequest("Invalid or missing DoctorId in the token.");
            }
            return await _doctorService.UpdateAppointment(doctorId, appointmentDTO);
        }


        [HttpDelete("api/[controller]/Appointments/[action]/{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment(int appointmentId)
        {
            var doctorIdClaim = HttpContext.User.FindFirst("DoctorId");

            if (!int.TryParse(doctorIdClaim.Value, out var doctorId))
            {
                return BadRequest("Invalid or missing DoctorId in the token.");
            }
            return await _doctorService.DeleteAppointment(appointmentId);

        }
       
    }
   
}
