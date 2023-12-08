using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Models
{
    public class Time
    {
        [Key]
        public int TimeId { get; set; }
        public string time { get; set; }

        public bool isBooked { get; set; } = false;

        public int AppointmentId {  get; set; }
        [JsonIgnore]
        public Appointment Appointment { get; set; }

        [JsonIgnore]
        public Booking Booking { get; set; }


    }
}
