using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.Services
{
    public interface ICouponService
    {
        Task<IActionResult> AddCoupon(CouponDTO couponDTO);
        
        Task<IActionResult> GetAllCoupons();
       
        Task<IActionResult> UpdateCoupon(CouponDTO couponDTO, int couponID);
        
        Task<IActionResult> DeleteCoupon(int couponId);
        
        Task<IActionResult> DeactivateCoupon(int couponId);
        
    }
}
