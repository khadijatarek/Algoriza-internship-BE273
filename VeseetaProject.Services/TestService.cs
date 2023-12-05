using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Core.Services;
using VeseetaProject.Data;

namespace VeseetaProject.Services
{
    public class TestService :ITestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var result = await _unitOfWork.Appointments.GetById(id);
            if(result == null)
            {
                return new NotFoundObjectResult($"Id {id} Not found");
            }
            else 
                return new OkObjectResult(result);
        }
        public async Task<IActionResult> GetAppointmentByIdWithTimesDoctor(int id)
        {
            var result = await _unitOfWork.Appointments2.getAppointmentWithTimes(id);
            if (result == null)
            {
                return new NotFoundObjectResult($"Id {id} Not found");
            }
            else
                return new OkObjectResult(result);
        }
        public async Task<IActionResult> GetDoctorsWithappointment(int id)
        {
            var result = await _unitOfWork.Appointments2.getDoctorwithAppointment(id);
            if (result == null)
            {
                return new NotFoundObjectResult($"Id {id} Not found");
            }
            else
                return new OkObjectResult(result);
        }
        public async Task<IActionResult> GetAllDoctorsWithappointment()
        {
            var result = await _unitOfWork.Appointments2.getAllDoctorswithAppointment();
            if (result == null)
            {
                return new NotFoundObjectResult(result);
            }
            else
                return new OkObjectResult(result);
        }

    }
}
