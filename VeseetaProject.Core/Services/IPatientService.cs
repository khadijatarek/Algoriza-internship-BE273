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
        Task<IEnumerable<PatientDetailsDTO>> GetAllPatients();// Expression<Func<Task, bool>>criteria, int pageSize, int page);
        Task<PatientDetailsDTO> GetPatientById(string id);
    }
}
