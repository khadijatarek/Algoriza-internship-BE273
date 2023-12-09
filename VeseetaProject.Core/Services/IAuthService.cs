using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.DTOs;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> LoginAsync(LoginDTO loginDTO);
        Task<IdentityResult> RegisterPatientAsync(PatientRegisterDTO userDTO,AccountType accountType= AccountType.Patient/*stringimage?*/);
        Task<IdentityResult> RegisterDoctorAsync(DoctorRegisterDTO registerDTO, string ImageUrl, AccountType type = AccountType.Doctor);
        Task<IActionResult> Registeration(RegisterationDTO userDTO);
        Task<IActionResult> Login(LoginDTO loginDTO);




    }
}
