using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeseetaProject.Core.DTOs
{
    public class DoctorResponse
    {
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public decimal? Price { get; set; }
        public string Gender { get; set; }
        public List<AppointmentResponse> Appointments { get; set; }
    }

    public class AppointmentResponse
    {
        public string Day { get; set; }
        public List<TimeResponse> Times { get; set; }
    }

    public class TimeResponse
    {
        public int Id { get; set; }
        public string Time { get; set; }
    }
}
