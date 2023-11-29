using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        
        
        
        public string PatientId { get; set; }
        public ApplicationUser Patient { get; set; }
        
        
        public int TimeId { get; set; }
        public Time Time { get; set; }


        public BookingStatus Status { get; set; }

        public int? CouponId { get; set; }
        public Coupon? Coupon { get; set; }
    }
}
