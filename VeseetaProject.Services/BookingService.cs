using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Core.Services;
using VeseetaProject.Data;

namespace VeseetaProject.Services
{
    public class BookingService :IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Booking>> GetAllDoctorBookings(int doctorId)
        {
            return await _unitOfWork.Bookings.GetAll(booking => booking.Time.Appointment.DoctorId == doctorId,
                null, null, new[] { "Time", "Time.Appointment", "Time.Appointment.Doctor", "Time.Appointment.DoctorId" } );
        }
    }
}
