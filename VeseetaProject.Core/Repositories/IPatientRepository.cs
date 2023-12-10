using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.Repositories
{
    public interface IPatientRepository : IBaseRepository<ApplicationUser>
    {
        int getNumberOfCompletedBookings(string patientId);
        ApplicationUser getPatientById(string patientId);

        Task<List<PatientBookingsDTO>> getPatientsBooking(string patientId);
    }
}
