
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Core.Services;
using VeseetaProject.Data;
using VeseetaProject.Services;

namespace VeseetaProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                            .AddJsonOptions(options=> options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(
                  builder.Configuration.GetConnectionString("DefaultConnection"),
                  b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //options.Password.RequiredLength = 8;
                //options.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IDoctorService, DoctorService>();
            builder.Services.AddTransient<IDashboardService, DashboardService>();
            builder.Services.AddTransient<IPatientService, PatientService>();
            builder.Services.AddTransient<ICouponService, CouponService>();
            builder.Services.AddTransient<IBookingService, BookingService>();
            builder.Services.AddTransient<ITestService, TestService>();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();


            app.MapControllers();

            app.Run();
        }
    }
}