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
        //        internal static void SeedAdminData(ModelBuilder modelBuilder, UserManager<ApplicationUser> userManager)
        //        {
        //            var adminUser = new ApplicationUser
        //            {
        //                Email = "admin@veseeta.com",
        //                UserName = "admin@veseeta.com",
        //                PasswordHash = "Admin@123",
        //                Type = AccountType.Admin,
        //            };

        //            string adminPassword = "Admin@123";
        //            if (userManager.FindByEmailAsync(adminUser.Email).Result == null)
        //            {
        //                var result = userManager.CreateAsync(adminUser, adminPassword).Result;
        //            modelBuilder.Entity<ApplicationUser>().HasData(var result = userManager.CreateAsync(adminUser, adminPassword).Result;
        //)


        //            }



        //        }

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
