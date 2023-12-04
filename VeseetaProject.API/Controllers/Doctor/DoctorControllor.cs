using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers.Doctor
{
    //[Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
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

        [HttpGet("GET DOCTOR BY ID")]
        public async Task<IActionResult> getbyId(int id)
        {
            return Ok(await _doctorService.GetDoctor_ById(id));
        }

      
    }
   
}
