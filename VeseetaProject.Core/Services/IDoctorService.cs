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
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDetailsDTO>> GetAllDoctors();
        Task<IActionResult> GetDoctorById(int id);
        Task<IActionResult> AddAppointment(int doctorId, AppointmentDTO appointmentDTO);
        Task<Booking> ConfirmCheckUpAsync(/*int doctorId,*/ int bookingId);
        Task<IActionResult> UpdateDoctor(DoctorRegisterDTO doctorDTO, int doctorId);

    }
}
