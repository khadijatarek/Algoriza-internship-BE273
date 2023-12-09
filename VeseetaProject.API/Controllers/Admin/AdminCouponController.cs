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
    [Authorize(Roles = "Admin")]
    public class AdminCouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public AdminCouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        [HttpPost("Add Coupon")]
        public async Task<IActionResult> AddCoupon([FromForm] CouponDTO couponDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var coupon = await _couponService.AddCoupon(couponDTO);
            return new OkObjectResult( new 
            {
                Success = true,
                Coupon = coupon
            });
        }
        [HttpPut("UpdateCoupon/{id}")]
        public async Task<IActionResult> UpdateCoupon([FromForm] CouponDTO coupon, int id)
        {
            if (ModelState.IsValid)
            {
                var result = await  _couponService.UpdateCoupon(coupon, id);
                return result ;
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteCoupon/{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            return await  _couponService.DeleteCoupon(id);
            
        }

        [HttpPut("DeactivateCoupon/{id}")]
        public async Task<IActionResult> DeactivateCoupon(int id)
        {
            return await  _couponService.DeactivateCoupon(id);
            
        }

        [HttpGet("Get All Coupons")]
        public async Task<IActionResult> GetAll()
        {
            return await _couponService.GetAllCoupons();
        }
    }
}
