using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.DTOs
{
    public class DoctorBookingsDTO
    {
        public string PatientName { get; set; }
        public string PatientImage { get; set; }
        public Days Day { get; set; }
        public string Time { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus BookingStatus { get; set; }

    }
}
