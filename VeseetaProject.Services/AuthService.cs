using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        //private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            IUnitOfWork unitOfWork,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<IActionResult> Registeration(RegisterationDTO userDTO)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = userDTO.Email,
                //ImageUrl = Image,
                UserName = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                PhoneNumber = userDTO.Phone,
                Gender = userDTO.Gender,
                Type = AccountType.Patient,
                DateOfBirth = userDTO.DateOfBirth,
            };
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
                    //check here
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
                    

                    var mytoken = GenerateJWTtoken(user);
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

       
       



        public async Task<IdentityResult> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);

                if (result.Succeeded)
                {
                    // Login successful
                    return IdentityResult.Success;
                }
            }

            // Login failed
            return IdentityResult.Failed(new IdentityError { Description = "Invalid login attempt." });
        }

        public async Task<IdentityResult> RegisterPatientAsync(PatientRegisterDTO patientDTO, AccountType accountType = AccountType.Patient)
        {
            var user = new ApplicationUser
            {

                FirstName = patientDTO.FirstName,
                LastName = patientDTO.LastName,
                PhoneNumber = patientDTO.Phone,
                ImageUrl = patientDTO.Image,
                Gender = patientDTO.Gender,
                UserName = patientDTO.Email,
                Email = patientDTO.Email,
                Type = accountType,
                DateOfBirth = patientDTO.DateOfBirth,
            };

            var result = await _userManager.CreateAsync(user, patientDTO.Password);
            if (result.Succeeded)
            {
                var assignRoleResult = await _userManager.AddToRoleAsync(user, "Patient");

            }

            return result;
        }
        public async Task<IdentityResult> RegisterDoctorAsync(DoctorRegisterDTO registerDTO, AccountType type = AccountType.Doctor)
        {
            var user = new ApplicationUser
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                PhoneNumber = registerDTO.Phone,
                //ImageUrl = registerDTO.Image,
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

        private string GenerateJWTtoken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();


            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            //get roles
            var roles = _userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //create token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWT:ValidIssuer").Value,
                audience: _configuration.GetSection("JWT:ValidAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
                );
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

        //private string SaveImageToFolder(IFormFile image)
        //{
        //    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
        //    var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
        //    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        image.CopyTo(stream);
        //    }

        //    return Path.Combine("Images", uniqueFileName);
        //}
    }
}
