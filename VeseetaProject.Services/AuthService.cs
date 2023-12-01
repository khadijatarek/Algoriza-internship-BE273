using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
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
                UserName = patientDTO.Email,
                Email = patientDTO.Email,
                Type = accountType,
            };

            var result = await _userManager.CreateAsync(user, patientDTO.Password);

            return result;
        }
        public async Task<IdentityResult> RegisterDoctorAsync(DoctorRegisterDTO registerDTO, AccountType type = AccountType.Doctor)
        {
            var user = new ApplicationUser
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                Type = type
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
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
     }

}
