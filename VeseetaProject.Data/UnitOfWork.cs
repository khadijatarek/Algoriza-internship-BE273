using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Data.Repositories;

namespace VeseetaProject.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Specialization> Specializations { get;}
        public IBaseRepository<ApplicationUser> Users { get;}
        public IDoctorRepository Doctors { get; }
        public IBaseRepository<Appointment> Appointments { get; }
        public IBaseRepository<Booking> Bookings { get; }
        public IBaseRepository<Coupon> Coupons { get; }

        public ITestRepository Appointments2 { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context =context;

            Specializations = new BaseRepository<Specialization>(context);
            Users = new BaseRepository<ApplicationUser>(context);
            //Doctors = new BaseRepository<Doctor>(context);
            Doctors = new DoctorRepository(context);
            Appointments = new BaseRepository<Appointment>(context);
            Bookings = new BaseRepository<Booking>(context);
            Coupons = new BaseRepository<Coupon>(context);
            Appointments2= new TestRepository(context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
