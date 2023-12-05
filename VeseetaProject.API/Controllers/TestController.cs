using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public Task<IActionResult> GetAppointment(int id)
        {
            return _testService.GetAppointmentById(id);
        }
        [HttpGet]
        public Task<IActionResult> GetAppointmentWithTimes(int id)
        {
            return _testService.GetAppointmentByIdWithTimesDoctor(id);
        }
        [HttpGet]
        public Task<IActionResult> GetDoctorsWithAppointments(int id)
        {
            return _testService.GetDoctorsWithappointment(id);
        }
        [HttpGet]
        public Task<IActionResult> GetAllDoctorsWithAppointments()
        {
            return _testService.GetAllDoctorsWithappointment();
        }

    }
}
