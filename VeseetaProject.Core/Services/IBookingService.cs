using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllDoctorBookings(int doctorId);
        Task<IEnumerable<DoctorResponse>> getAvailableAppointments();
        Task<Booking> addBooking(string patientId, int timeId,string? discountCode);
    }
}
