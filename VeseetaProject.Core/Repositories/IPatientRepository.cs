using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.Repositories
{
    public interface IPatientRepository : IBaseRepository<ApplicationUser>
    {
        int getNumberOfCompletedBookings(string patientId);
    }
}
