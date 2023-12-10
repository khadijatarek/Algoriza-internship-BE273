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

        public async Task<IActionResult> AddCoupon(CouponDTO couponDTO)
        {
            // Build the coupon
            var coupon = new Coupon()
            {
                Type = couponDTO.Type,
                Value = couponDTO.Value,
                NumOfBookings = couponDTO.NumOfBookings,
                DiscountCode = couponDTO.DiscountCode,
                //is active
                IsActive = true,
                //not used
                IsUsed = false,
            };

            var result = await _unitOfWork.Coupons.Add(coupon);

            if (result != null)
            {
                _unitOfWork.Complete();

                // added
                return new OkObjectResult(new
                {
                    Success = true,
                    Coupon = result
                });
            }
            else
            {
                // couldnt add a coupon
                return new BadRequestObjectResult(new
                {
                    Success = false,
                    Message = "Failed to add coupon"
                });
            }
        }
        
        public async Task<IActionResult> GetAllCoupons()
        {
            var result = await  _unitOfWork.Coupons.GetAll(null, null, null, null);
            return new OkObjectResult(result);
        }

        public async Task<IActionResult> UpdateCoupon(CouponDTO couponDTO, int couponID)
        {

            //Get the coupon
            var existingCoupon = await _unitOfWork.Coupons.Find(c => c.CouponId == couponID);

            if (existingCoupon != null)
            {
                // Check if the coupon is already used
                if (!existingCoupon.IsUsed)
                {
                    //save updated data
                    existingCoupon.Value = couponDTO.Value;
                    existingCoupon.DiscountCode = couponDTO.DiscountCode;
                    existingCoupon.Type = couponDTO.Type;
                    existingCoupon.NumOfBookings = couponDTO.NumOfBookings;

                    // Update the coupon
                    _unitOfWork.Coupons.Update(existingCoupon);
                    _unitOfWork.Complete();

                    // Updated Successfully
                    return new OkObjectResult(new { IsSuccess = true, Message = "Coupon updated successfully" });
                }
                else
                {
                    //cant update used coupon
                    return new ObjectResult(new { IsSuccess = false, Message = "Can't update a used Coupon" })
                    {
                        StatusCode = 403
                    };
                }

            }
            else
            {
                // Not found
                return new NotFoundObjectResult("Coupon Doesn't Exist");
            }
        }


        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            // Get the coupon by ID
            var existingCoupon = await _unitOfWork.Coupons.Find(c => c.CouponId == couponId);

            if (existingCoupon != null)
            {
                // Check if the coupon is already used
                if (!existingCoupon.IsUsed)
                {
                    // Delete the coupon
                    _unitOfWork.Coupons.DeleteById(couponId);
                    _unitOfWork.Complete();

                    // deleted
                    return new OkObjectResult(new { IsSuccess = true, Message = "Coupon deleted successfully" });
                }
                else
                {
                    // cant delete
                    return new ObjectResult(new { IsSuccess = false, Message = "Cannot delete a used coupon" })
                    {
                        StatusCode = 403
                    };
                }
            }
            else
            {
                //not found
                return new NotFoundObjectResult("Coupon doesn't exist");
            }
        }

        public async Task<IActionResult> DeactivateCoupon(int couponId)
        {
            // Get the coupon by ID
            var existingCoupon =await  _unitOfWork.Coupons.Find(c => c.CouponId == couponId);

            if (existingCoupon != null)
            {
                // deactivate
                existingCoupon.IsActive = false;

                // Update the coupon
                _unitOfWork.Coupons.Update(existingCoupon);
                _unitOfWork.Complete();

                // deactivated
                return new OkObjectResult(new { IsSuccess = true, Message = "Coupon deactivated successfully" });
            }
            else
            {
                // not found
                return new NotFoundObjectResult("Coupon doesn't exist");
            }
        }
    }

   
}
