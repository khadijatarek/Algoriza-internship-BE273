using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Models
{
    public class Time
    {
        [Key]
        public int TimeId { get; set; }
        public string time { get; set; }

        public int AppointmentId {  get; set; }
        public Appointment Appointment { get; set; }

        public Booking Booking { get; set; }
    }
}
