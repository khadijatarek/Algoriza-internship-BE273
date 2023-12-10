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

        public async Task<IEnumerable<DoctorResponse>> getAvailableAppointments(int? pageNum=1, int? pageSize=null,string? searchBy=null)
        {
            var doctorResponses = await _unitOfWork.Doctors.getAllAppointmentsAndDoctorDetails(pageNum,pageSize,searchBy);
            return doctorResponses;

        }

        public async Task<IActionResult> GetAllDoctorBookings(int doctorId, int? pageNum = 1, int? pageSize = null, string? searchBy = null)
        {
            var doctor = await _unitOfWork.Doctors.GetDoctorById(doctorId);

            if (doctor == null)
            {
                return new NotFoundObjectResult("Doctor Not Found");
            }
            else
            {
                var doctorBookings = await _unitOfWork.Doctors.GetDoctorBookings(doctorId, pageNum, pageSize, searchBy);
                if(doctorBookings == null)
                {
                    return new OkObjectResult("No bookings yet");
                }
                else
                {
                    return new OkObjectResult(doctorBookings);
                }
            }
               
          
        
        }


        public async Task<IActionResult> CancelBooking(int bookingId, string patientId)
        {
            var booking = await _unitOfWork.Bookings.GetById(bookingId);
            if(booking == null)
            {
                return new NotFoundObjectResult($"Booking {bookingId} doesn't exist");
            }
            else
            {
                if(booking.PatientId != patientId)
                {
                    return new BadRequestObjectResult($"Booking {bookingId} isn't your, you can't cancel booking that isn't yours");
                }
                else
                {
                    if(booking.Status == BookingStatus.Pending)
                    {
                        booking.Status = BookingStatus.Canceled;
                        _unitOfWork.Bookings.Update(booking);

                        var time = await  _unitOfWork.Times.GetById(booking.TimeId);
                        time.isBooked = false;
                        _unitOfWork.Times.Update(time);
                        
                        _unitOfWork.Complete();
                        return new OkObjectResult(new
                        {
                            Success = true,
                            Message = "Booking Canceled",
                            Booking = booking
                        });
                    }
                    else
                    {
                        return new BadRequestObjectResult("You can only cancel pending bookings");
                    }
                }
            }
        }

        public async Task<IActionResult> GetAllPatientBookings(string patientId)
        {
            var bookings =await _unitOfWork.Bookings.GetAll(b => b.PatientId == patientId, null,null);
            if(bookings== null)
            {
                return new OkObjectResult("No bookings yet");
            }
           
           else return new OkObjectResult ( new 
           { 
               bookings = await _unitOfWork.Patients.getPatientsBooking(patientId) 
           });
        }


        private decimal GetPriceAfterDiscount(decimal price, Coupon coupon)
        {
            decimal totalPrice;
            if (coupon.Type == DiscountType.Percentage)
            {
                totalPrice = price * (1 - coupon.Value / 100);
            }
            else
            {
                totalPrice = price - coupon.Value;
            }
            return totalPrice;
        }
        private bool IsCouponEligible(Coupon coupon, string patientId)
        {
            var numOfPatientBookings = _unitOfWork.Patients.getNumberOfCompletedBookings(patientId);
            if (numOfPatientBookings >= coupon.NumOfBookings)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
