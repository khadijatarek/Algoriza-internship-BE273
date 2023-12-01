using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDetailsDTO>> GetAllDoctors();
        Task<DoctorDetailsDTO> GetDoctorById(int id);
    }
}
