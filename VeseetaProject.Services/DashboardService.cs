using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Core.Services;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Services
{
    public class DashboardService :IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> getDoctorsNum()
        {
            return await _unitOfWork.Doctors.Count(null);
        }
        public async Task<int> getDoctorsNumfromAppUser()
        {
            return await _unitOfWork.Users.Count(u => u.Type ==AccountType.Doctor);
        }
        public async Task<int> getPatientsNum()
        {
            return await _unitOfWork.Users.Count(u=>u.Type==AccountType.Patient);
        }
    }
}
