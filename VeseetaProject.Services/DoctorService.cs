using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
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
        private readonly IImageService _imageService;

        public DoctorService(IUnitOfWork unitOfWork, IAuthService authService, IImageService imageService)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }
        public async Task<IEnumerable<DoctorDetailsDTO>> GetAllDoctors(int? pageNum = 1, int? pageSize = null)
        {
            var doctors = await _unitOfWork.Doctors.GetAll(null, pageNum, pageSize, new[] { "User", "Specialization" });
            return doctors.Select(d => new DoctorDetailsDTO
            {
                Image = d.User.ImageUrl,
                FullName = $"{d.User.FirstName} {d.User.LastName}",
                Email = d.User.Email,
                Phone = d.User.PhoneNumber,
                Specialization = d.Specialization.NameEn,
                Gender = d.User.Gender
            });
        }
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetDoctorById(id);
            if (doctor != null)
            {
                DoctorDetailsDTO doctorDTO = new DoctorDetailsDTO
                {
                    Email = doctor.User.Email,
                    Image = doctor.User.ImageUrl,
                    FullName = $"{doctor.User.FirstName} {doctor.User.LastName}",
                    Phone = doctor.User.PhoneNumber,
                    Specialization = doctor.Specialization.NameEn,
                    Gender = doctor.User.Gender,
                   
                };
                return new OkObjectResult(new 
                {
                    Success= true,
                    Doctor = doctorDTO
                });

            }
            else
            {
                return new NotFoundObjectResult($"No doctor with id {id}");
            }
        }

        public async Task<IActionResult> UpdateDoctor(DoctorRegisterDTO doctorDTO, int doctorId, string ImageUrl)
        {
            var existingDoctor = await _unitOfWork.Doctors.GetDoctorById(doctorId);
            var existingPhoto = existingDoctor.User.ImageUrl;
            if (existingDoctor != null)
            {
                existingDoctor.User.ImageUrl = ImageUrl;
                existingDoctor.User.FirstName = doctorDTO.FirstName;
                existingDoctor.User.LastName = doctorDTO.LastName;
                existingDoctor.User.Email = doctorDTO.Email;
                existingDoctor.User.PhoneNumber = doctorDTO.Phone;
                existingDoctor.User.Gender = doctorDTO.Gender;
                existingDoctor.User.DateOfBirth = doctorDTO.DateOfBirth;
                existingDoctor.SpecializationId = doctorDTO.SpecializationId;
                existingDoctor.User.UserName = doctorDTO.Email.ToUpper();
                existingDoctor.User.NormalizedUserName = doctorDTO.Email.ToUpper();
                existingDoctor.User.NormalizedEmail = doctorDTO.Email.ToUpper();

                _unitOfWork.Doctors.Update(existingDoctor);
                _unitOfWork.Complete();
                //Delete Existing photo
                _imageService.DeletePhoto(existingPhoto);

                return new OkObjectResult(new
                {
                    Success = true,
                    Message = "Doctor UpdatedSuccessfully"
                });
            }
            else
            {
                // Not found
                return new NotFoundObjectResult(value: "Doctor Doesn't Exist");
            }
        }
        public async Task<IActionResult>  DeleteDoctor(int doctorId)
        {
            var existingDoctor = await _unitOfWork.Doctors.GetDoctorById(doctorId);
            if (existingDoctor != null)
            {

                if (existingDoctor.Appointments != null)
                { 
                    var appointments = existingDoctor.Appointments;
                    foreach (Appointment appointment in appointments)
                    {

                        foreach (Time time in appointment.Times)
                        {
                            if (time.isBooked)
                            {
                                return new BadRequestObjectResult("Can't Delete Doctor with Bookings(Requests)");
                            }

                        }

                    }
                   
                }

                var userId = existingDoctor.UserId;
                var image = existingDoctor.User.ImageUrl;
                _imageService.DeletePhoto(image);
                _unitOfWork.Doctors.DeleteById(doctorId);
                _unitOfWork.Users.DeleteById(userId);
                _unitOfWork.Complete();

                return new OkObjectResult(new
                {
                    Success = true,
                    Message = "Doctor Deleted"
                });
            }
            else
            {
                return new NotFoundObjectResult("Doctor doesn't exist");
            }
        }



        //public async Task<IActionResult> GetAllBookings(int doctorId,int? pageNum, int? pageSize, string? search)
        //{
        //    var doctor = await _unitOfWork.Doctors.GetById(doctorId);

        //}
        public async Task<IActionResult> AddAppointment(int doctorId, AppointmentDTO appointmentDTO)
        {
            var doctor = await _unitOfWork.Doctors.GetById(doctorId);
            if (doctor == null)
            {
                return new NotFoundObjectResult("Please Login");
            }
            else
            {
                //check if doctor already have appointments on that day 
                foreach (var daySlot in appointmentDTO.Appointments)
                {
                    var day = MapStringToDay(daySlot.Day);
                    if (await _unitOfWork.Doctors.DoctorHasAppointmentOnDay(doctorId, day))
                    {
                        return new BadRequestObjectResult($"You already have an appointment on {day}, Please enter any other Day");
                    }
                }
                List<Appointment> appointments = appointmentDTO.Appointments.Select(appointmentSlot =>
                new Appointment
                {
                    DoctorId = doctorId,
                    Day = MapStringToDay(appointmentSlot.Day),
                    Times = appointmentSlot.Times.Select(timeString =>
                    new Time
                    {
                        time = timeString,
                    }
                    ).ToList(),
                }).ToList();
                var addedAppointments = await _unitOfWork.Appointments.AddRange(appointments);
                if (addedAppointments != null)
                {
                    if (appointmentDTO.Price.HasValue)
                    {
                        doctor.Price = appointmentDTO.Price;
                    }
                    _unitOfWork.Complete();
                    return new OkObjectResult(new {
                        Success = true,
                        Appointments = addedAppointments,
                    });
                }
                return new BadRequestObjectResult("Sonething Went Wrong");
            }

        }




        public async Task<IActionResult> UpdateAppointment(int appointmentId, AppointmentDTO updatedAppointmentDTO)
        {
            var existingAppointment = await _unitOfWork.Appointments.GetById(appointmentId);
            if (existingAppointment == null)
            {
                return new NotFoundObjectResult("Appointment not found");
            }

            // Check if the appointment is booked (pending or completed)
            if (existingAppointment.Times.Any(time => time.isBooked))
            {
                return new BadRequestObjectResult("Cannot update a booked appointment");
            }

            // Check if the updated times are available
            foreach (var daySlot in updatedAppointmentDTO.Appointments)
            {
                var day = MapStringToDay(daySlot.Day);
                if (existingAppointment.Times.Any(time =>
                    time.isBooked &&
                    daySlot.Times.Contains(time.time)))
                {
                    return new BadRequestObjectResult($"Cannot update times for booked slots on {day}");
                }
            }

            // Update the appointment
            existingAppointment.Day = MapStringToDay(updatedAppointmentDTO.Appointments.First().Day);
            

            _unitOfWork.Complete();
            return new OkObjectResult(new { Success = true, Message = "Appointment updated successfully" });
        }

        public async Task<IActionResult> UpdateTime(int timeId, Time updatedTime)
        {
            var existingTime = await _unitOfWork.Times.GetById(timeId);
            if (existingTime == null)
            {
                return new NotFoundObjectResult("Time not found");
            }

            // Check if the time is part of a booked appointment (pending or completed)
            if (existingTime.isBooked)
            {
                return new BadRequestObjectResult("Cannot update a booked time");
            }

            // Update the time
            existingTime.time = updatedTime.time;
            
            _unitOfWork.Complete();
            return new OkObjectResult(new { Success = true, Message = "Time updated successfully" });
        }

        public async Task<IActionResult> DeleteTime(int timeId)
        {
            var timeToDelete = await _unitOfWork.Times.GetById(timeId);
            if (timeToDelete == null)
            {
                return new NotFoundObjectResult("Time not found");
            }

            // Check if the time is part of a booked appointment (pending or completed)
            if (timeToDelete.isBooked)
            {
                return new BadRequestObjectResult("Cannot delete a booked time");
            }

            // Perform the deletion
            _unitOfWork.Times.DeleteById(timeId);
            _unitOfWork.Complete();
            return new OkObjectResult(new { Success = true, Message = "Time deleted successfully" });
        }

        public async Task<IActionResult> DeleteAppointment(int appointmentId)
        {
            var appointmentToDelete = await _unitOfWork.Appointments.GetById(appointmentId);
            if (appointmentToDelete == null)
            {
                return new NotFoundObjectResult("Appointment not found");
            }

            // Check if the appointment is booked (pending or completed)
            if (appointmentToDelete.Times.Any(time => time.isBooked))
            {
                return new BadRequestObjectResult("Cannot delete a booked appointment");
            }

            // Perform the deletion
            _unitOfWork.Appointments.DeleteById(appointmentId);
            _unitOfWork.Complete();
            return new OkObjectResult(new { Success = true, Message = "Appointment deleted successfully" });
        }


        public async Task<IActionResult> ConfirmCheckUpAsync(int doctorId, int bookingId)
        {
            var doctor = await _unitOfWork.Doctors.GetDoctorById(doctorId);

            if (doctor == null || doctor.Appointments == null)
            {
                //appointments are null
                return new NotFoundObjectResult("Doctor or appointments not found");
            }

            foreach (var appointment in doctor.Appointments)
            {
                if (appointment.Times != null)
                {
                    foreach (var time in appointment.Times)
                    {
                        if (time.Booking != null && time.Booking.BookingId == bookingId)
                        {
                            var booking = time.Booking;

                            if (booking.Status == BookingStatus.Pending)
                            {
                                booking.Status = BookingStatus.Completed;
                                _unitOfWork.Bookings.Update(booking);
                                _unitOfWork.Complete();
                                return new OkObjectResult(new
                                {
                                    Success = true,
                                    Message = "Booking Confirmed",
                                    booking = booking
                                }); 
                            }
                            else
                            {
                                return new BadRequestObjectResult("Booking is not in Pending status");
                            }
                        }
                    }
                }
            }

            // Handle the case where the booking with the specified ID is not found for the doctor
            return new NotFoundObjectResult("Booking not found for the specified doctor");
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
