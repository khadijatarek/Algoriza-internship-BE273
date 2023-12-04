using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public AdminCouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        [HttpPost("AddCoupon")]
        public async Task<IActionResult> addCoupon(CouponDTO couponDTO)
        {
            if (couponDTO == null)
            {
                return BadRequest();
            }
            return Ok( await _couponService.AddCoupon(couponDTO));
        }
    }
}
