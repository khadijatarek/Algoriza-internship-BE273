using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Services;

namespace VeseetaProject.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AdminCouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public AdminCouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        [HttpPost("AddCoupon")]
        public async Task<IActionResult> addCoupon([FromForm] CouponDTO couponDTO)
        {
            if (couponDTO == null)
            {
                return BadRequest();
            }
            return Ok( await _couponService.AddCoupon(couponDTO));
        }
        [HttpPut("UpdateCoupon")]
        public ActionResult updateCoupon([FromForm] Coupon coupon)
        {
            return Ok(_couponService.UpdateCoupon(coupon));
            //throw new NotImplementedException();
        }
        [HttpDelete("DeleteCoupon")]
        public ActionResult deleteCoupom(int couponId)
        {
            return Ok(_couponService.DeleteCoupon(couponId));
            //throw new NotImplementedException();
        }
        [HttpPut("DeactivateCoupon")]
        public ActionResult deactivateCoupon(int couponId)
        {
            return Ok(_couponService.DeactivateCoupon(couponId));
            //throw new NotImplementedException();
        }
    }
}
