using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;
using VeseetaProject.Data.Configurations;

namespace VeseetaProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Coupon> Coupons { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configure
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new DoctorConfiguration());
            builder.ApplyConfiguration(new AppointmentConfiguration());
            builder.ApplyConfiguration(new TimeConfiguration());
            builder.ApplyConfiguration(new BookingConfiguration());
            builder.ApplyConfiguration(new CouponConfiguration());

            //Seed Data 
            DataSeeder.SeedSpecializations(builder);
          
            
            base.OnModelCreating(builder);


        }

    }
}
