using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Services
{
    public interface IImageService
    {
        string SaveImageToFolder(IFormFile image, string Username);
        void DeletePhoto(string photoUrl);
    }
}
