using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.DTOs
{
    public class CouponDTO
    {
        public string Type { get; set; }
        public int NumOfBookings { get; set; }
        public string DiscountCode { get; set; }
        public decimal? Value { get; set; }


    }
}
