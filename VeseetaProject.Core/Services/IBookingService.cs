using Microsoft.AspNetCore.Mvc;
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
        Task<IActionResult> GetAllDoctorBookings(int doctorId, int? pageNum = 1, int? pageSize = null, string? searchBy = null);
       
        Task<IActionResult> GetAllPatientBookings(string patientId);
        
        Task<IEnumerable<DoctorResponse>> getAvailableAppointments(int? pageNum = 1, int? pageSize = null, string? searchBy = null);
        
        Task<IActionResult> addBooking(string patientId, int timeId, string? discountCode);
        
        Task<IActionResult> CancelBooking(int bookingId, string patientId);

    }
}
