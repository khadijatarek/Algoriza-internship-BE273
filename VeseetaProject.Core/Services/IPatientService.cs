using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDetailsDTO>> GetAllPatients(int? pageNum = 1, int? pageSize = null);
        
        Task<PatientDetailsDTO> GetPatientById(string id);
    }
}
