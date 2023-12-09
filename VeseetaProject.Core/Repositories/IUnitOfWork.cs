using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.Repositories
{
    public interface IUnitOfWork :IDisposable
    {
        IBaseRepository<Specialization> Specializations { get; }
        IBaseRepository<ApplicationUser> Users { get; }
        IDoctorRepository Doctors { get; }
        IBaseRepository<Appointment> Appointments { get; }
        IBaseRepository<Booking> Bookings { get; }
        IBaseRepository<Coupon> Coupons { get; }
        IPatientRepository Patients { get; }
        //IBaseRepository<Time> Times { get; }

        int Complete();

    }
}
