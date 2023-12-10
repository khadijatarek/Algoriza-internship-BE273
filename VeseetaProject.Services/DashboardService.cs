using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Core.Services;
using VeseetaProject.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VeseetaProject.Services
{
    public class DashboardService :IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> getDoctorsNum()
        {
            return await _unitOfWork.Doctors.Count(null);
        }
        public async Task<int> getPatientsNum()
        {
            return await _unitOfWork.Users.Count(u=>u.Type==AccountType.Patient);
        }
        public async Task<IActionResult> getReqestsNum()
        {
            var numOfRequests = await _unitOfWork.Bookings.Count(null);
            var numOfCompletedRequests = await _unitOfWork.Bookings.Count(booking => booking.Status == BookingStatus.Completed);
            var numOfCanceledRequests = await _unitOfWork.Bookings.Count(booking => booking.Status == BookingStatus.Canceled);
            var numOfPendingRequests = await _unitOfWork.Bookings.Count(booking => booking.Status == BookingStatus.Pending);
            return new OkObjectResult(new
            {
                NumOfAllRequests = numOfRequests,
                NumOfCompletedRequests = numOfCompletedRequests,
                NumOfCanceledRequests = numOfCanceledRequests,
                numOfPendingRequests = numOfPendingRequests
            });
        }
        public async Task<IActionResult> GetTop5Specializations()
        {
            var topSpecializations = await _unitOfWork.Doctors.GetTop5Specializations();

            var result = topSpecializations
                .Select(s => new SpecializationDto
                 {
                     SpecializationName = s.NameEn,
                     NumOfDoctors = s.Doctors.Count(),
                     NumBookings = s.Doctors.Sum(d=>d.Appointments.Count(a=>a.Times.Any(t=>t.isBooked)))
                    
                 });

            return new OkObjectResult(result);
        }


        public async Task<IActionResult> GetTop10Doctors()
        {
            var topDoctors = await _unitOfWork.Doctors.GetTop10Doctors();

            var result = topDoctors.Select(d => new DoctorInfoDto
            {
                Image = d.User?.ImageUrl,
                FullName = $"{d.User.FirstName} {d.User.LastName}",
                Specialization = d.Specialization?.NameEn,
                NumRequests = d.Appointments.Count(a => a.Times.Any(t => t.isBooked))
            });

            return new OkObjectResult(result);
        }






    }
    public class SpecializationDto
    {
        public string SpecializationName { get; set; }
        public int NumOfDoctors { get; set; }
        public int NumBookings { get; set; }
    }

    public class DoctorInfoDto
    {
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public int NumRequests { get; set; }
    }
}
