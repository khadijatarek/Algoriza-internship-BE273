using Microsoft.AspNetCore.Mvc;
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
        Task<IActionResult> getReqestsNum();
        Task<IActionResult> getTop10Doctors();
        Task<IActionResult> getTop5Specializations();
    }
}
