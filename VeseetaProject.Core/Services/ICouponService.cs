﻿using System;
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
        Task<Coupon> AddCoupon(CouponDTO couponDTO);
        Coupon UpdateCoupon(Coupon coupon);
        bool DeleteCoupon(int couponId);
        Task<bool> DeactivateCoupon(int couponId);

    }
}