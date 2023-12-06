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
        public string Type { get; set; }
        public int NumOfBookings { get; set; }
        public string DiscountCode { get; set; }
        public decimal Value { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(DiscountType))]
        public DiscountType Type1 {  get; set; }


    }
}
