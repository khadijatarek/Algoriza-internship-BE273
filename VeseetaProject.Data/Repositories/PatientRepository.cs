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
    public class PatientRepository : BaseRepository<ApplicationUser>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public int getNumberOfCompletedBookings(string patientId)
        {
            var patient = _context.Users
                .Include(p => p.Bookings)
                .FirstOrDefault(p => p.Id == patientId);
            int count;
            if (patient.Bookings == null)
            {
                count = 0;
            }
            else {
                count = patient.Bookings.Count(b => b.Status == BookingStatus.Completed); 
            }
            return count;
                
        }
    }
}
