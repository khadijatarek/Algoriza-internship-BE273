using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.DTOs
{
    public class BookingDTO
    {
        public int TimeId { get; set; }
        public string? Coupon { get; set; }
    }
}
