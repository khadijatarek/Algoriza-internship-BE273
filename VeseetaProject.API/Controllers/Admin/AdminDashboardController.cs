using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers.Admin
{

    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
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

        [HttpGet("NumOfRequests")]
        public async Task<IActionResult> getNumOfrequests()
        {
            return await _dashboardService.getReqestsNum();
        }

        [HttpGet("Top5Specializations")]
        public async Task<IActionResult> Top5Specializations()
        {
            return await _dashboardService.GetTop5Specializations();
        }

        [HttpGet("Top10Doctors")]
        public async Task<IActionResult> Top10Doctors()
        {
            return await _dashboardService.GetTop10Doctors();

        }

    }
}
