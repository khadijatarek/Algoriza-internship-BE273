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
        public async Task<DoctorDetailsDTO> GetDoctorById(int id)
        {
            
            throw new NotImplementedException();
            
            //var doctor = await _unitOfWork.Doctors.GetById(id);
            //DoctorDetailsDTO d = new DoctorDetailsDTO {
            //    Email = doctor.User.Email,
            //    Image = doctor.User.Image,
            //    FullName = $"{doctor.User.FirstName} {doctor.User.LastName}",
            //    Phone = doctor.User.PhoneNumber,
            //    Specialization = doctor.Specialization.NameEn,
            //    Gender = doctor.User.Gender
            //};
            
            //return d;
        }

    }
}
