using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.DTOs
{
    public class AppointmentDTO
    {
        public decimal? Price { get; set; }
        public List<DaySlotsDTO> Appointments { get; set; }
    }

    public class DaySlotsDTO
    {
        public string Day { get; set; }
        public List<string> Times { get; set; }
    }
}
