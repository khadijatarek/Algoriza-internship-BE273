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
    public class ApplicationUserConfiguration :IEntityTypeConfiguration<ApplicationUser>
    {

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.DateOfBirth)
                   .HasColumnType("date");
           
            // Convert enum to string
            builder.Property(u => u.Type)
                   .HasConversion<string>(); 
            builder.Property(u => u.Gender)
                   .HasConversion<string>();
        }
    }
}
