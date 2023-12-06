using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers.Patient
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost("Add Booking")]
        public async Task<IActionResult> AddBooking([FromForm]BookingDTO bookingDTO, string patientId)
        {
            var result =await _bookingService.addBooking(patientId, bookingDTO.TimeId, bookingDTO.Coupon);
            return Ok(result);
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
