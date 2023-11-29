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
    public class DoctorConfiguration: IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            // Configure one to many relationship with Specialization 
            builder.HasOne(d => d.Specialization)
                   .WithMany(s => s.Doctors)
                   .HasForeignKey(d => d.SpecializationId);

            builder.Property(d => d.Price)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
