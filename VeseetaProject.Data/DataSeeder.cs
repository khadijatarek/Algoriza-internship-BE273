using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Data
{
    public static class DataSeeder
    {
        internal static void SeedAdminData(ModelBuilder modelBuilder)
        {
            var adminUser = new ApplicationUser
            {
                Email = "admin@veseeta.com",
                UserName = "admin@veseeta.com",
                LastName = "admin",
                FirstName = "admin",
                PhoneNumber = "1234567890",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2000, 1, 1),
                Type = AccountType.Admin,
            };
            
            string adminPassword = "Admin@123";
            adminUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(adminUser,adminPassword);
            
            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "Admin", 
                UserId = adminUser.Id,
            });

        }

        internal static void SeedRoles(ModelBuilder modelBuilder)
        {
            string[] roles = { "Admin", "Patient", "Doctor" };

            foreach (var roleName in roles)
            {
                modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = roleName, NormalizedName = roleName.ToUpper() });
            }
        }

        internal static void SeedSpecializations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { SpecializationId = 1, NameEn = "Dentist", NameAr = "طبيب أسنان" },
                new Specialization { SpecializationId = 2, NameEn = "Cardiologist", NameAr = "أخصائي قلب" },
                new Specialization { SpecializationId = 3, NameEn = "Dermatologist", NameAr = "أخصائي جلدية" },
                new Specialization { SpecializationId = 4, NameEn = "Pediatrician", NameAr = "طبيب أطفال" },
                new Specialization { SpecializationId = 5, NameEn = "Orthopedic Surgeon", NameAr = "جراح عظام" },
                new Specialization { SpecializationId = 6, NameEn = "Neurologist", NameAr = "أخصائي أعصاب" },
                new Specialization { SpecializationId = 7, NameEn = "Gynecologist", NameAr = "طبيب نسائي" },
                new Specialization { SpecializationId = 8, NameEn = "Psychiatrist", NameAr = "طبيب نفسي" },
                new Specialization { SpecializationId = 9, NameEn = "Ophthalmologist", NameAr = "طبيب عيون" }

            );
        }
    }
}
