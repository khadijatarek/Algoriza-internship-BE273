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
        Task<IdentityResult> RegisterPatientAsync(PatientRegisterDTO userDTO,AccountType accountType= AccountType.Patient);
        Task<IdentityResult> RegisterDoctorAsync(DoctorRegisterDTO registerDTO, AccountType type = AccountType.Doctor);


    }
}
