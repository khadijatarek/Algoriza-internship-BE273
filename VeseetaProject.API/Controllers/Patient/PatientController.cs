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
       
        
        
        //Not needed here
        [HttpGet("Get Patient By Id")]
        public ActionResult getAllPatients(string patientId)
        {
            var result = _patientService.GetPatientById(patientId);
            return Ok(result);
        }

        [HttpGet("Get All available appointments")]
        //public ActionResult getAllAppointments(int page, int pageSize, string search)
        public async Task<IActionResult> getAllAppointments()
        {
            var results = await _bookingService.getAvailableAppointments();
            if (results != null)
            {
                return Ok(results);
            }
            return BadRequest();
        }
        //[HttpGet("GetAllAvailableAppointments")]
        //public async Task<IActionResult> GetAllAppointments(int page, int pageSize, string search)
        //{
        //    try
        //    {
        //        var results = await _bookingService.getAvailableAppointments();
        //        if (results != null)
        //        {
        //            var doctorAppointments = results.Select(appointment =>
        //            {
        //                var doctor = appointment.Doctor;
        //                return new
        //                {
        //                    Doctor = new
        //                    {
        //                        doctor.User.ImageUrl,
        //                        FullName = $"{doctor.User.FirstName} {doctor.User.LastName}",
        //                        doctor.User.Email,
        //                        doctor.User.PhoneNumber,
        //                        Specialization = doctor.Specialization.Name,
        //                        doctor.Price,
        //                        doctor.User.Gender,
        //                    },
        //                    Appointments = new List<object>
        //            {
        //                new
        //                {
        //                    Day = appointment.Day,
        //                    Times = appointment.Times.Select(time => new { time.TimeId, time.time }).ToList(),
        //                }
        //            }
        //                };
        //            }).ToList();

        //            return Ok(doctorAppointments);
        //        }

        //        return BadRequest("No available appointments found.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it according to your application's requirements
        //        return StatusCode(500, "An error occurred while processing the request.");
        //    }
        //}


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
        public ActionResult getAllBookings(string patientId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("CancelBooking")]
        public ActionResult CancelUserBooking(int bookingId)
        {
            throw new NotImplementedException();
        }

    }
}
