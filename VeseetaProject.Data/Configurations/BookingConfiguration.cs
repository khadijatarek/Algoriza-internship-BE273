using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // Configure one to many relationship with booking
            builder.HasOne(b => b.Patient)
                   .WithMany(u => u.Bookings)  
                   .HasForeignKey(b => b.PatientId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Restrict);


            // Configure one to many relationship with Coupon
            builder.HasOne(b => b.Coupon)
                   .WithMany(c => c.Bookings)
                   .HasForeignKey(b => b.CouponId)
                   .IsRequired(false); // booking can exist without a coupon

            // Configure one to one relationship with time
            builder.HasOne(b => b.Time)
                   .WithOne(t => t.Booking)
                   .HasForeignKey<Booking>(b => b.TimeId)
                   .OnDelete(DeleteBehavior.Restrict); 
            
            //Save enum as string
            builder.Property(b => b.Status)
                   .HasConversion<string>();
        }
    }
}
