using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.DTOs
{
    public class DoctorRegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int SpecializationId { get; set; }
    }
}
