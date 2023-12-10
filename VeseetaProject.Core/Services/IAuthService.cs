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
        Task<IActionResult> Registeration(RegisterationDTO userDTO, string? ImageUrl=null);

        Task<IActionResult> Login(LoginDTO loginDTO);

        
        Task<IdentityResult> RegisterDoctorAsync(DoctorRegisterDTO registerDTO, string ImageUrl, AccountType type = AccountType.Doctor);



    }
}
