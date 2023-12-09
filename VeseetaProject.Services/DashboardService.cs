using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Core.Services;
using VeseetaProject.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
            var numOfCompletedRequests = await _unitOfWork.Bookings.Count(booking =>booking.Status==BookingStatus.Completed);
            var numOfCanceledRequests = await _unitOfWork.Bookings.Count(booking => booking.Status == BookingStatus.Canceled);
            var numOfPendingRequests = await _unitOfWork.Bookings.Count(booking => booking.Status == BookingStatus.Pending);
            return new OkObjectResult(new
            {
                NumOfAllRequests = numOfCompletedRequests,
                NumOfCompletedRequests = numOfCompletedRequests,
                NumOfCanceledRequests = numOfCanceledRequests,
                numOfPendingRequests = numOfPendingRequests
            });
        }

        public Task<IActionResult> getTop10Doctors()
        {
            throw new NotImplementedException();
        }
        public Task<IActionResult> getTop5Specializations()
        {
            throw new NotImplementedException();
        }
    }
}
