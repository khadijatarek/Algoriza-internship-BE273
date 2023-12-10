using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeseetaProject.Core.DTOs
{
    
    public class SpecializationDto
    {
        public string SpecializationName { get; set; }
        public int NumOfDoctors { get; set; }
        public int NumBookings { get; set; }
    }

    public class DoctorInfoDto
    {
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public int NumRequests { get; set; }
    }
}
