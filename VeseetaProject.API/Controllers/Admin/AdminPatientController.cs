using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.Services;
using VeseetaProject.Services;

namespace VeseetaProject.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class AdminPatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public AdminPatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("Get All Patients")]
        public async Task<IActionResult> getAllPatients()// int pageSize, int page)
        {
            var result = await _patientService.GetAllPatients();
            
            if(result != null) {
                return Ok(result);
            }
            else return BadRequest();
        }

        [HttpGet("GetPatientById")]
        public async Task<IActionResult> getPatientById(string id)
        {
            //LESSA HETET EL REQUESTS
            var result = await _patientService.GetPatientById(id);

            if (result != null)
            {
                return Ok(result);
            }
            else return BadRequest();
        }
    }
}
