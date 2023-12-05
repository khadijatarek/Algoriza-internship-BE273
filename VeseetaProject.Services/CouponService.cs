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


        public Coupon UpdateCoupon(Coupon coupon)
        {
            var result = _unitOfWork.Coupons.Update(coupon);
            _unitOfWork.Complete();
            if (result != null)
            {
                return result;
            }
            else return null;
        }

        public async Task<bool> DeactivateCoupon(int couponId)
        {
            var coupon = await _unitOfWork.Coupons.GetById(couponId);
            coupon.IsActive = false;
            UpdateCoupon(coupon);
            return true;
        }

        public bool DeleteCoupon(int couponId)
        {
            _unitOfWork.Coupons.DeleteById(couponId);
            return true;
        }
    }
}
