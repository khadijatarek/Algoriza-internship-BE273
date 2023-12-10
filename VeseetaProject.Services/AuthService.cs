using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;
using VeseetaProject.Core.Repositories;
using VeseetaProject.Core.Services;
using VeseetaProject.Data;

namespace VeseetaProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;


        public AuthService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        
        
        public async Task<IActionResult> Registeration(RegisterationDTO userDTO, string?ImageUrl)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = userDTO.Email,
                UserName = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                PhoneNumber = userDTO.Phone,
                Gender = userDTO.Gender,
                Type = AccountType.Patient,
                DateOfBirth = userDTO.DateOfBirth,
            };
            if (ImageUrl != null)
            {
                user.ImageUrl = ImageUrl;
            }

            var result = await _userManager.CreateAsync(user,userDTO.Password);
            if(result.Succeeded)
            {
                var assignRole = await _userManager.AddToRoleAsync(user, AccountType.Patient.ToString());
                if(assignRole.Succeeded)
                {
                    return new OkObjectResult(result);
                }
                else 
                { 
                    return new JsonResult(assignRole);
                }
            }
            return new JsonResult(result);
        }


        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            //first check if the user exists 
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user != null) 
            {
                bool checkUserPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

                if (!checkUserPassword)
                {
                    return new UnauthorizedObjectResult(new {
                        LoginSuccess = false,
                        Message="Incorrect Email or Password"
                        
                    });
                }
                else
                {
                    

                    var mytoken = GenerateJwtToken(user);
                    return new OkObjectResult(new
                    {
                        LoginSuccess = true,
                        token = mytoken
                        //Valid to
                    });
                }
            }
            else
            {
                return new UnauthorizedObjectResult("Invalid Login");
            }

        }

        public async Task<IdentityResult> RegisterDoctorAsync(DoctorRegisterDTO registerDTO, string ImageUrl, AccountType type = AccountType.Doctor)
        {
            var user = new ApplicationUser
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                PhoneNumber = registerDTO.Phone,
                ImageUrl = ImageUrl,
                Gender = registerDTO.Gender,
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                Type = type,
                DateOfBirth = registerDTO.DateOfBirth,
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Doctor");

                var userId = await _userManager.GetUserIdAsync(user);

                Doctor doctor = new Doctor()
                {
                    UserId = userId,
                    SpecializationId = registerDTO.SpecializationId
                };

                var doctorResult = await _unitOfWork.Doctors.Add(doctor);
                if (doctorResult != null)
                {
                    _unitOfWork.Complete();
                    return IdentityResult.Success; // Return success
                }
            }

            return result; // Return the original result with errors
        }


        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value);


            // build claims
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            });

            //check if user is doctor to add doctor ID to jwt 
            if (user.Type == AccountType.Doctor)
            {
                var doctorId = _unitOfWork.Doctors.GetDoctorIdFromUserId(user.Id);
                if (doctorId != null)
                {
                    claims.AddClaim(new Claim("DoctorId", doctorId.ToString()));
                }
            }

            //get roles
            var roles = _userManager.GetRolesAsync(user).Result;

            foreach (var role in roles)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            // create a token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _configuration.GetSection("JWT:ValidIssuer").Value,
                Audience = _configuration.GetSection("JWT:ValidAudience").Value,
                Subject = claims,
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }


        private static Gender ParseGender(string genderString)
        {
            if (string.IsNullOrWhiteSpace(genderString))
            {
                // Default to some value (you can change this based on your requirements)
                return Gender.Male;
            }

            if (Enum.TryParse<Gender>(genderString, true, out var gender))
            {
                return gender;
            }

            throw new ArgumentException($"Invalid gender: {genderString}", nameof(genderString));
        }

       
    }
}
