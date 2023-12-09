using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Services;

namespace VeseetaProject.Services
{
    public class ImageService :IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string SaveImageToFolder(IFormFile image, string Username)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Username + "_" + image.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return Path.Combine("Images", uniqueFileName);
        }
        public void DeletePhoto(string photoUrl)
        {
            var filePath =  Path.Combine(_webHostEnvironment.WebRootPath, photoUrl);
            System.IO.File.Delete(filePath);
        }

    }
}
