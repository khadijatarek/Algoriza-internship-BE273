﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Gender = doctor.User.Gender
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

        public async Task<IActionResult> UpdateDoctor(DoctorRegisterDTO doctorDTO, int doctorId)
        {
            var existingDoctor = await _unitOfWork.Doctors.GetDoctorById(doctorId);
            if(existingDoctor != null)
            {
                //existingDoctor.User.ImageUrl = doctorDTO.Image
                existingDoctor.User.FirstName = doctorDTO.FirstName;
                existingDoctor.User.LastName = doctorDTO.LastName;
                existingDoctor.User.Email = doctorDTO.Email;
                existingDoctor.User.PhoneNumber = doctorDTO.Phone;
                existingDoctor.User.Gender = doctorDTO.Gender;
                existingDoctor.User.DateOfBirth = doctorDTO.DateOfBirth;
                existingDoctor.SpecializationId = doctorDTO.SpecializationId;

                _unitOfWork.Doctors.Update(existingDoctor);
                _unitOfWork.Complete();
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
        //public async Task<IActionResult> DeleteDoctor(int doctorId)
        //{
        //    var existingDoctor = await _unitOfWork.Doctors.Find(doctor => doctor.DoctorId == doctorId) ;
        //    if(existingDoctor != null)
        //    {
        //        //check if doctor has appointments, then check if he has requests
        //        //if(existingDoctor.Appointments!=null)
        //    }
        //    else
        //    {
        //        //not found
        //        return new NotFoundObjectResult("Doctor doesn't exist");
        //    }
        //}



        //public async Task<IEnumerable<Appointment>> AddAppointment(int doctorId, AppointmentDTO appointmentDTO)
        //{
        //    try
        //    {
        //        var doctor = await _unitOfWork.Doctors.GetById(doctorId);
        //        if (doctor == null)
        //        {
        //            return null;
        //        }
        //        List<Appointment> appointments = appointmentDTO.Appointments.Select(appointmentSlot =>
        //        new Appointment
        //        {
        //            DoctorId = doctorId,
        //            Day = MapStringToDay(appointmentSlot.Day),
        //            Times = appointmentSlot.Times.Select(timeString =>
        //            new Time
        //            {
        //                time = timeString,
        //            }
        //            ).ToList(),
        //        }
        //        ).ToList();
        //        var result = await _unitOfWork.Appointments.AddRange(appointments);
        //        if (appointmentDTO.Price.HasValue)
        //        {
        //            doctor.Price = appointmentDTO.Price;
        //        }
        //        _unitOfWork.Complete();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

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
