using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }

        public DiscountType Type { get; set; }
        public int NumOfBookings { get; set; }
        public string DiscountCode { get; set; }

        public decimal? Value { get; set; }
        public bool IsUsed { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
