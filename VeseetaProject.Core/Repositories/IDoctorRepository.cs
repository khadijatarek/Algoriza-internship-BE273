using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.Repositories
{
    public interface IDoctorRepository :IBaseRepository<Doctor>
    {        
        Task<Doctor> GetDoctorById(int id);
        int GetDoctorIdFromUserId(string userId);

       Task<IEnumerable<DoctorResponse>> getAllAppointmentsAndDoctorDetails(int? pageNum = 1, int? pageSize = null, string? searchBy = null);
        decimal GetAppointmentPrice(int timeId);

        Task<bool> DoctorHasAppointmentOnDay(int doctorId, Days day);
       
        
        Task<IEnumerable<Specialization>> GetTop5Specializations();
        Task<IEnumerable<Doctor>> GetTop10Doctors();
   
        Task<List<DoctorBookingsDTO>> GetDoctorBookings(int doctorId, int? pageNum = 1, int? pageSize = null, string? searchBy = null);
      
    }
}
