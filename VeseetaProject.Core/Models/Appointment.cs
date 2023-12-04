using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public Days Day { get; set; }

        public ICollection<Time> Times { get; set; }
        [JsonIgnore]
        public Doctor Doctor { get; set; }
    }
}
