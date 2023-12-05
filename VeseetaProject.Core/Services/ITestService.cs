using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Services
{
    public interface ITestService
    {
        Task<IActionResult> GetAppointmentById(int id);
        Task<IActionResult> GetAppointmentByIdWithTimesDoctor(int id);
        Task<IActionResult> GetDoctorsWithappointment(int id);
        Task<IActionResult> GetAllDoctorsWithappointment();
    }
}
