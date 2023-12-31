﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.DTOs
{
    public class PatientDetailsDTO
    {

        public string? Image { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Gender? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
