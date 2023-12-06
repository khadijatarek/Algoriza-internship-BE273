using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Core.Services;

namespace VeseetaProject.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;


        public DoctorService(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<DoctorDetailsDTO>> GetAllDoctors()
        {
            var doctors = await _unitOfWork.Doctors.GetAll(null, null, null, new[] { "User", "Specialization" });
            return doctors.Select(d => new DoctorDetailsDTO
            {
                Image = d.User.Image,
                FullName = $"{d.User.FirstName} {d.User.LastName}",
                Email = d.User.Email,
                Phone = d.User.PhoneNumber,
                Specialization = d.Specialization.NameEn,
                Gender = d.User.Gender
            });
        }
        //public async Task<DoctorDetailsDTO> GetDoctorById(int id)
        //{

        //    var doctor = await _unitOfWork.Doctors.GetById(id);
        //    DoctorDetailsDTO d = new DoctorDetailsDTO {
        //        Email = doctor.User.Email,
        //        Image = doctor.User.Image,
        //        FullName = $"{doctor.User.FirstName} {doctor.User.LastName}",
        //        Phone = doctor.User.PhoneNumber,
        //        Specialization = doctor.Specialization.NameEn,
        //        Gender = doctor.User.Gender
        //    };
        //    return d;

        //    throw new NotImplementedException();
            
        //    //var doctor = await _unitOfWork.Doctors.GetById(id);
        //    //DoctorDetailsDTO d = new DoctorDetailsDTO {
        //    //    Email = doctor.User.Email,
        //    //    Image = doctor.User.Image,
        //    //    FullName = $"{doctor.User.FirstName} {doctor.User.LastName}",
        //    //    Phone = doctor.User.PhoneNumber,
        //    //    Specialization = doctor.Specialization.NameEn,
        //    //    Gender = doctor.User.Gender
        //    //};
            
        //    //return d;
        //}

        public async Task<Doctor> GetDoctor_ById(int id)
        {
            return await _unitOfWork.Doctors.GetById(id);

        }

        public async Task<IEnumerable<Appointment>> AddAppointment(int doctorId, AppointmentDTO appointmentDTO)
        {
            try
            {
                var doctor = await _unitOfWork.Doctors.GetById(doctorId);
                if (doctor == null)
                {
                    return null;
                }
                List<Appointment> appointments = appointmentDTO.Appointments.Select(appointmentSlot =>
                new Appointment
                {
                    DoctorId = doctorId,
                    Day = MapStringToDay(appointmentSlot.Day), 
                    Times= appointmentSlot.Times.Select(timeString =>
                    new Time
                    {
                        time = timeString,
                    }
                    ).ToList(),
                }
                ).ToList();
                var result =await _unitOfWork.Appointments.AddRange(appointments);
                if (appointmentDTO.Price.HasValue)
                {
                    doctor.Price = appointmentDTO.Price;
                }
                _unitOfWork.Complete();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<Booking> ConfirmCheckUpAsync(/*int doctorId,*/ int bookingId)
        {
            
            var booking =await _unitOfWork.Bookings.GetById(bookingId);

            if (booking.Status == BookingStatus.Pending)// check on doctor id bardo
            {
                booking.Status = BookingStatus.Completed;
                var result = _unitOfWork.Bookings.Update(booking);
                _unitOfWork.Complete();
                return booking;
                //if (result.Status == BookingStatus.Completed)
                //{
                //    return true;
                //}
                //else
                //    return false;
            }
            else
                return booking;
        }

        private Days MapStringToDay(string dayString)
        {
            if (Enum.TryParse<Days>(dayString, out var day))
            {
                return day;
            }
           throw new ArgumentException($"Invalid day: {dayString}");
        }
    }
    
}
