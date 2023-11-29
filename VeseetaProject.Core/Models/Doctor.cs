using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public decimal Price { get; set; }

        //one to one relationship
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        //one to one relationship
        public Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }

        // one to many relationship
        public ICollection<Appointment> Appointments { get; set; }


    }
}
