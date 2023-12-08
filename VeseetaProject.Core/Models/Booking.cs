using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public BookingStatus Status { get; set; }

        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        public string PatientId { get; set; }
        [JsonIgnore]
        public ApplicationUser Patient { get; set; }
        

        public int TimeId { get; set; }
        public Time Time { get; set; }

        public int? CouponId { get; set; }
        [JsonIgnore]
        public Coupon? Coupon { get; set; }
    }
}
