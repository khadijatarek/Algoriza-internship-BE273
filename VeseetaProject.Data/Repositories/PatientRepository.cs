using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Repositories;

namespace VeseetaProject.Data.Repositories
{
    public class PatientRepository : BaseRepository<ApplicationUser>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationUser getPatientById(string patientId)
        {
            var patient = _context.Users
                .Include(p => p.Bookings)
                .FirstOrDefault(p => p.Id == patientId);
            return patient;
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

        public async Task<List<PatientBookingsDTO>> getPatientsBooking(string patientId)
        {
            var user = await _context.Users
                .Include(p => p.Bookings)
                .ThenInclude(b => b.Time)
                .ThenInclude(t => t.Appointment)
                .ThenInclude(a => a.Doctor)
                .ThenInclude(d => d.User)
                .FirstOrDefaultAsync(p => p.Id == patientId);

            if (user != null)
            {
                var patientBookings = new List<PatientBookingsDTO>();
                foreach (var booking in user.Bookings)
                {
                    //var specializationName = booking.Time.Appointment.Doctor.Specialization.NameEn;
                    var patientBookingDTO = new PatientBookingsDTO
                    {
                        DoctorImage = booking.Time.Appointment.Doctor.User.ImageUrl,
                        DoctorName = $"{booking.Time.Appointment.Doctor.User.FirstName} {booking.Time.Appointment.Doctor.User.LastName}",
                        //DoctorSpecialization = specializationName,
                        day = booking.Time.Appointment.Day,
                        time = booking.Time.time,
                        Price = booking.Price,
                        PriceTotal = booking.TotalPrice,
                        BookingStatus = booking.Status
                    };

                    patientBookings.Add(patientBookingDTO);
                }
                return patientBookings;
            }
            return null;
        }

    }
}
