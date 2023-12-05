using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.Repositories
{
    public interface ITestRepository: IBaseRepository<Appointment>
    {
        IEnumerable<Appointment> getAllAppointmentsWithDetails();
        Task<Appointment> getAppointmentWithTimes(int id);
        Task<Doctor> getDoctorwithAppointment(int id);
        Task<List<Doctor>> getAllDoctorswithAppointment();
    }
}
