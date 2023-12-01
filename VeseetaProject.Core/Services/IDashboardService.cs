using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Services
{
    public interface IDashboardService
    {
        Task<int> getDoctorsNum();
        Task<int> getPatientsNum();
        Task<int> getDoctorsNumfromAppUser();
    }
}
