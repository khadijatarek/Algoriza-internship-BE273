using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult getAllAppointments()
        {
            throw new NotImplementedException();
        }

        [HttpPost("Add Booking")]
        public ActionResult AddBooking(Booking booking)
        {
            throw new NotImplementedException();
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
