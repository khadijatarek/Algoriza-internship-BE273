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
        //Admin/Doctor
        Task<IEnumerable<DoctorDetailsDTO>> GetAllDoctors(int? pageNum = 1, int? pageSize = null);
        Task<IActionResult> GetDoctorById(int id);
        Task<IActionResult> UpdateDoctor(DoctorRegisterDTO doctorDTO, int doctorId, string ImageUrl);
        //Delete doctor

        //Doctor
        Task<IActionResult> AddAppointment(int doctorId, AppointmentDTO appointmentDTO);
        
        Task<IActionResult> ConfirmCheckUpAsync(int doctorId, int bookingId);

        Task<IActionResult> UpdateAppointment(int appointmentId, AppointmentDTO updatedAppointmentDTO);
        Task<IActionResult> UpdateTime(int timeId, Time updatedTime);
        Task<IActionResult> DeleteTime(int timeId);
        Task<IActionResult> DeleteAppointment(int appointmentId);




    }
}
