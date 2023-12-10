using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers.Patient
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Patient")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IBookingService _bookingService;

        public PatientController(IPatientService patientService, IBookingService bookingService)
        {
            _patientService = patientService;
            _bookingService = bookingService;
        }

        [HttpGet("Get All Appointments and Times")]
        public async Task<IActionResult> getAllAppointments(int? pageNum, int? pageSize, string? search)
        {
            var results = await _bookingService.getAvailableAppointments(pageNum,pageSize,search);
            if (results != null)
            {
                return Ok(results);
            }
            return BadRequest();
        }

        [HttpPost("Add Booking")]
        public async Task<IActionResult> AddBooking([FromForm] BookingDTO bookingDTO)
        {
            if (ModelState.IsValid)
            {
                // Get the PatientId claim from the JWT token
                var patientIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var patientId = patientIdClaim.Value;
                
                if (patientIdClaim != null)
                {
                    var result = await _bookingService.addBooking(patientId, bookingDTO.TimeId, bookingDTO.Coupon);
                    return result;
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest("Invalid ModelState");
            }
        }

        [HttpGet("getAllUserBookings")]
        public Task<IActionResult> getAllBookings()
        {
            // Get the PatientId claim from the JWT token
            var patientIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var patientId = patientIdClaim.Value;

            return _bookingService.GetAllPatientBookings(patientId);
        }

        [HttpPut("CancelBooking")]
        public Task<IActionResult> CancelUserBooking(int bookingId)
        {
            var patientIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var patientId = patientIdClaim.Value;

            return _bookingService.CancelBooking(bookingId, patientId);  
        }

    }
}
