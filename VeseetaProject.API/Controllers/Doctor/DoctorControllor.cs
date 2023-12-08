using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers.Doctor
{
    //[Authorize("doctor")]
    //[Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> Add(int doctorId,AppointmentDTO appointmentDTO)
        {
            return Ok(await _doctorService.AddAppointment(doctorId,appointmentDTO));
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
