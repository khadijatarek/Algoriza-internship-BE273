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
        ApplicationUser getPatientById(string patientId);

        int getNumberOfCompletedBookings(string patientId);
       
        Task<List<PatientBookingsDTO>> getPatientsBooking(string patientId);
    }
}
