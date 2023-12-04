using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Core.Services;

namespace VeseetaProject.Services
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CouponService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Coupon> AddCoupon(CouponDTO couponDTO)
        {
            var coupon = new Coupon()
            {
                Value = couponDTO.Value,
                NumOfBookings = couponDTO.NumOfBookings,
                DiscountCode = couponDTO.DiscountCode
            };
            var result = _unitOfWork.Coupons.Add(coupon);
            if(result != null)
            {
                _unitOfWork.Complete();
            }
            return result;
        }


        public Task<Coupon> UpdateCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeactivateCoupon(int couponId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCoupon(int couponId)
        {
            throw new NotImplementedException();
        }
    }
}
