using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.DTOs
{
    public class CouponDTO
    {
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(DiscountType))]
        public DiscountType Type { get; set; }
        [Required]
        public int NumOfBookings { get; set; }
        [Required]
        public string DiscountCode { get; set; }
        [Required]
        public decimal Value { get; set; }


    }
}
