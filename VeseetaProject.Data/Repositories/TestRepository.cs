using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Repositories;

namespace VeseetaProject.Data.Repositories
{
    public class TestRepository : BaseRepository<Appointment>, ITestRepository
    {
        public TestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Appointment> getAllAppointmentsWithDetails()
        {
            throw new NotImplementedException();
        }

        public async Task<Appointment> getAppointmentWithTimes(int id)
        {
            var result = await _context
                 .Appointments
                 //.Where(a => a.AppointmentId == id)
                 .Include("Times")
                 .Include("Doctor")
                 .FirstOrDefaultAsync(a=>a.AppointmentId==id);
               
           return result;
        }
        public async Task<Doctor> getDoctorwithAppointment(int id)
        {
            var result = await _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Specialization)
                .Include(d => d.Appointments)
                .ThenInclude(a => a.Times)
                .FirstOrDefaultAsync(d => d.DoctorId == id);
            return result;
        }
        public async Task<List<Doctor>> getAllDoctorswithAppointment()
        {
            var result = await _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Specialization)
                .Include(d => d.Appointments)
                .ThenInclude(a => a.Times)
                .ToListAsync();
            return result;
        }
    }
}
