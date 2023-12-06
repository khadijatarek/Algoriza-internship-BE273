using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.Repositories
{
    public interface IDoctorRepository :IBaseRepository<Doctor>
    {
        Task<IEnumerable<DoctorResponse>> getAllAppointmentsAndDoctorDetails();
        decimal GetAppointmentPrice(int timeId);
        //int GetDoctorIdFromBooking(int bookingId);
    }
}
