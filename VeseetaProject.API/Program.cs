
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
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
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IDoctorService, DoctorService>();
            builder.Services.AddTransient<IDashboardService, DashboardService>();
            builder.Services.AddTransient<IPatientService, PatientService>();
            builder.Services.AddTransient<ICouponService, CouponService>();
            builder.Services.AddTransient<IBookingService, BookingService>();
            builder.Services.AddTransient<IImageService, ImageService>();


            //JWT Configuration 
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                  .AddJwtBearer(jwt =>
                  {
                      var key = Encoding.ASCII
                         .GetBytes(builder.Configuration.GetSection("JWT:key").Value);

                      jwt.SaveToken = true;
                      jwt.TokenValidationParameters = new TokenValidationParameters()
                      {
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(key),
                          ValidateIssuer = false, 
                          ValidateAudience = false, 
                          RequireExpirationTime = false,
                          ValidateLifetime = true
                      };
                  });




            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Veseeta", Version = "v1" });
                swagger.SwaggerDoc("V2",new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Veseeta",
                });
                swagger.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT Bearer token",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

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