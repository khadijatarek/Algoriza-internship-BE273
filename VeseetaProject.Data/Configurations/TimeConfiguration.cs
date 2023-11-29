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
    public class TimeConfiguration : IEntityTypeConfiguration<Time>
    {
        public void Configure(EntityTypeBuilder<Time> builder)
        {
            // Configure one to one relationship with Booking 
            builder.HasOne(t => t.Booking)
                   .WithOne(b => b.Time)
                   .HasForeignKey<Booking>(b => b.TimeId);
        }
    }
}
