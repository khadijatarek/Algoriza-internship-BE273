using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Core.Services;

namespace VeseetaProject.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PatientDetailsDTO>> GetAllPatients(int? pageNum = 1,int? pageSize=null)
        {
            var patients = await _unitOfWork.Users.GetAll(u => u.Type == Core.Models.AccountType.Patient, pageNum, pageSize, new[] {"Bookings"}); //, page, null);

            return patients.Select(p => new PatientDetailsDTO
            {
                Image = p.ImageUrl,
                Email = p.Email,
                FullName = $"{p.FirstName} {p.LastName}",
                Phone = p.PhoneNumber,
                Gender = p.Gender,
                Bookings = p.Bookings
            });
        }

        public async Task<PatientDetailsDTO> GetPatientById(string id)
        {
            var patient = _unitOfWork.Patients.getPatientById(id);

            PatientDetailsDTO patientDetails = new PatientDetailsDTO
            {
                Image = patient.ImageUrl,
                Email = patient.Email,
                FullName = $"{patient.FirstName} {patient.LastName}",
                Phone = patient.PhoneNumber,
                Gender = patient.Gender,
                Bookings = patient.Bookings
            };
            return patientDetails ;
        }
    }
}
