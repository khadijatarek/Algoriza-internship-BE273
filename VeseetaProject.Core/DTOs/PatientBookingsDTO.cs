using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.DTOs
{
    public class PatientBookingsDTO
    {
        public string DoctorImage { get; set; }
        public string DoctorName { get; set;}
        public string DoctorSpecialization { get; set;}
        public Days day { get; set; }
        public string time { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTotal { get; set;}
        public BookingStatus BookingStatus { get; set; }
    }
}
