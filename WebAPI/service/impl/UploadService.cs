using System.IO;
using Microsoft.AspNetCore.Http;
using WebAPI.utils;

namespace WebAPI.service.impl {
    public class UploadService : IUploadService {

        public string SaveImage(IFormFile image) {
            string relativePath = Path.Combine(AppSettings.ImagePath, image.FileName);
            string newName = FileUtils.SaveFile(image, Path.Combine(AppSettings.FolderPath, relativePath));
            return Path.Combine(AppSettings.ImagePath, newName);
        }
    }
}
