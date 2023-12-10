using Azure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Core.Services;
using VeseetaProject.Data;

namespace VeseetaProject.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public async Task<Booking> addBooking(string patientId, int timeId, string? discountCode)
        //{
        //    var bookingPrice = _unitOfWork.Doctors.GetAppointmentPrice(timeId);
        //    var booking = new Booking()
        //    {
        //        PatientId = patientId,
        //        TimeId = timeId,
        //        Status = BookingStatus.Pending,
        //        Price = bookingPrice,

        //    };
        //    var time = await _unitOfWork.Times.GetById(timeId);
        //    time.isBooked = true;
        //    _unitOfWork.Times.Update(time);

        //    if (!string.IsNullOrEmpty(discountCode))
        //    {
        //        var coupon = await _unitOfWork.Coupons
        //            .Find(c => c.DiscountCode == discountCode);
        //        if (coupon != null && IsCouponEligible(coupon, patientId))
        //        {
        //            booking.Coupon = coupon;
        //            booking.TotalPrice = GetPriceAfterDiscount(bookingPrice, coupon);
        //            coupon.IsUsed = true;
        //            _unitOfWork.Coupons.Update(coupon);

        //        }

        //    }
        //    else
        //    {
        //        booking.TotalPrice = bookingPrice;
        //    }
        //    booking = await _unitOfWork.Bookings.Add(booking);
        //    _unitOfWork.Complete();
        //    return booking;
        //}
        public async Task<IActionResult> addBooking(string patientId, int timeId, string? discountCode)
        {
            var time = await _unitOfWork.Times.GetById(timeId);
            if (!time.isBooked)
            {
                var bookingPrice = _unitOfWork.Doctors.GetAppointmentPrice(timeId);
                var booking = new Booking()
                {
                    PatientId = patientId,
                    TimeId = timeId,
                    Status = BookingStatus.Pending,
                    Price = bookingPrice,

                };
               

                if (!string.IsNullOrEmpty(discountCode))
                {
                    var coupon = await _unitOfWork.Coupons
                        .Find(c => c.DiscountCode == discountCode);
                    if (coupon != null)
                    {
                        if (IsCouponEligible(coupon, patientId))
                        {
                            booking.Coupon = coupon;
                            booking.TotalPrice = GetPriceAfterDiscount(bookingPrice, coupon);
                            coupon.IsUsed = true;
                            _unitOfWork.Coupons.Update(coupon);
                          }
                        else
                        {
                            return new BadRequestObjectResult("Coupon is not eligible");
                        }
                    }
                    else
                    {
                        return new BadRequestObjectResult("Invalid Discount Code");
                    }

                }
                else
                {
                    booking.TotalPrice = bookingPrice;
                }
                booking = await _unitOfWork.Bookings.Add(booking);

                time.isBooked = true;
                _unitOfWork.Times.Update(time);

                _unitOfWork.Complete();
                return new OkObjectResult(new
                {
                    Success = true,
                    booking = booking,
                });
            }
            else
            {
                return new BadRequestObjectResult($"Can't book this{timeId}, It's already booked");
            }
        }

        public async Task<IEnumerable<DoctorResponse>> getAvailableAppointments()
        {
            var doctorResponses = await _unitOfWork.Doctors.getAllAppointmentsAndDoctorDetails();
            return doctorResponses;

        }

        public async Task<IEnumerable<Booking>> GetAllPatientBookings(int patientId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Booking>> GetAllDoctorBookings(int doctorId)
        {
            return await _unitOfWork.Bookings.GetAll(booking => booking.Time.Appointment.DoctorId == doctorId,
                null, null, new[] { "Time", "Time.Appointment", "Time.Appointment.Doctor", "Time.Appointment.DoctorId" } );
        }




        private bool IsCouponEligible(Coupon coupon, string patientId)
        {
            var numOfPatientBookings = _unitOfWork.Patients.getNumberOfCompletedBookings(patientId);
            if(numOfPatientBookings >= coupon.NumOfBookings)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private decimal GetPriceAfterDiscount(decimal price, Coupon coupon)
        {
            decimal totalPrice;
            if(coupon.Type == DiscountType.Percentage)
            {
                totalPrice = price * (1 - coupon.Value / 100);
            }
            else
            {
                totalPrice = price - coupon.Value;
            }
            return totalPrice;
        }
    }

}
