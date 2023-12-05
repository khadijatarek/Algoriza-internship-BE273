﻿using System;
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
        public IBaseRepository<ApplicationUser> Users { get; }
        //IBaseRepository<Doctor> Doctors { get; }
        public IDoctorRepository Doctors { get; }
        IBaseRepository<Appointment> Appointments { get; }
        IBaseRepository<Booking> Bookings { get; }
        IBaseRepository<Coupon> Coupons { get; }

        ITestRepository Appointments2 { get; }

        int Complete();

    }
}
