﻿using Microsoft.EntityFrameworkCore;
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
                    Image = doctor.User.Image,
                    FullName = $"{doctor.User.FirstName} {doctor.User.LastName}",
                    Email = doctor.User.Email,
                    Phone = doctor.User.PhoneNumber,
                    Specialization = doctor.Specialization.NameEn, // You can choose the appropriate language
                    Price = doctor.Price,
                    Gender = doctor.User.Gender?.ToString(),
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
        public async Task<Doctor> getAllAppointmentsAndDoctorDetails2()
        {
            var doctors = await _context.Doctors.FindAsync(3);
            //.Include(d => d.User)
            //.Include(d => d.Specialization)
            //.Include(d => d.Appointments)
                //.ThenInclude(d => d.Times)
            //.ToListAsync();
            return doctors;
        }
    }
}
