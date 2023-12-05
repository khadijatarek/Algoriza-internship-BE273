using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Models
{
    public class Specialization
    {
        [Key]
        public int SpecializationId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        [JsonIgnore]
        public ICollection<Doctor> Doctors { get; set; }
    }

}
