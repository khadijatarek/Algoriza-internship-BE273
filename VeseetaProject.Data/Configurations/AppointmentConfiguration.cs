using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Data.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            // Configure one to many relationship with Time
            builder.HasMany(a => a.Times)
                   .WithOne(t => t.Appointment)
                   .HasForeignKey(t => t.AppointmentId);
          
             builder.Property(a => a.Day)
            .HasConversion<string>();
        }
    }
}
