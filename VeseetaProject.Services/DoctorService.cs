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
        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            return await _unitOfWork.Doctors.GetAll(null, null, null, null);
        }

    }
}
