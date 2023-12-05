using Azure;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Core.Services;
using VeseetaProject.Data;

namespace VeseetaProject.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Booking> addBooking(string patientId, int timeId)
        {
            var booking = await _unitOfWork.Bookings.Add(new Booking {
                PatientId = patientId,
                TimeId = timeId });
            return booking;
        }

        public async Task<IEnumerable<DoctorResponse>> getAllDoctorsAppointments()
        {
            var doctorResponses = await _unitOfWork.Doctors.getAllAppointmentsAndDoctorDetails();
            return doctorResponses;

        }

        public async Task<IEnumerable<Booking>> GetAllPatientBookings(int patientId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Booking>> GetAllDoctorBookings(int doctorId)
        {
            return await _unitOfWork.Bookings.GetAll(booking => booking.Time.Appointment.DoctorId == doctorId,
                null, null, new[] { "Time", "Time.Appointment", "Time.Appointment.Doctor", "Time.Appointment.DoctorId" } );
        }
    }
  
}
