using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Repositories;

namespace VeseetaProject.Data.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DoctorResponse>> getAllAppointmentsAndDoctorDetails()
        {
            var doctors = await _context.Doctors
            .Include(d => d.User)
            .Include(d => d.Specialization)
            .Include(d => d.Appointments)
                .ThenInclude(d => d.Times)
            .ToListAsync();

            var Response = doctors.Select(doctor =>
                new DoctorResponse
                {
                    Image = doctor.User.ImageUrl,
                    FullName = $"{doctor.User.FirstName} {doctor.User.LastName}",
                    Email = doctor.User.Email,
                    Phone = doctor.User.PhoneNumber,
                    Specialization = doctor.Specialization.NameEn,
                    Price = doctor.Price,
                    Gender = doctor.User.Gender.ToString(),
                    Appointments = doctor.Appointments.Select(appointment => new AppointmentResponse
                    {
                        Day = appointment.Day.ToString(),
                        Times = appointment.Times.Select(time => new TimeResponse
                        {
                            Id = time.TimeId,
                            Time = time.time
                        }).ToList()
                    }).ToList()
                });
            return Response;
        }
        
        public decimal GetAppointmentPrice(int timeId)
        {
            var time =  _context.Times
            .Where(t => t.TimeId == timeId)
            .Include(t => t.Appointment)
                .ThenInclude(a => a.Doctor)
            .FirstOrDefault();

            var price = time.Appointment.Doctor.Price;
            return (decimal)price;
        }


        public int GetDoctorIdFromUserId(string userId)
        {
            var user = _context.Users.Find(userId);
            var doctorId = _context.Doctors.FirstOrDefault(t => t.UserId == userId).DoctorId;
            return doctorId;
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            var doctor = await _context.Doctors
                .Where(d => d.DoctorId == id)
                .Include(d => d.User)
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync();
            return doctor;
        }
    }
}
