using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers.Admin
{

    [Route("api/[controller]")]
    [Authorize(Roles ="admin")]
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
        public ActionResult getNumOfrequests()
        {
            throw new NotImplementedException();
        }
        [HttpGet("Top5Specializations")]
        public ActionResult Top5Specializations()
        {
            throw new NotImplementedException();
        }
        [HttpGet("Top10Doctors")]
        public ActionResult Top10Doctors()
        {
            throw new NotImplementedException();
        }

    }
}
