using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public AdminDashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("NumOfDoctors")]
        public async Task<ActionResult<int>> getNumofDoctors()
        {
           return await _dashboardService.getDoctorsNum();
        }

        [HttpGet("NumOfPatients")]
        public async Task<ActionResult<int>> getNumOfPatients()
        {
            return await _dashboardService.getPatientsNum();
        }
        
      }
}
