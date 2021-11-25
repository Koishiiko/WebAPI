using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using WebAPI.utils;
using System.IO;

namespace WebAPI.service.impl {
    public class UploadService : IUploadService {

        public string SaveImage(IFormFile image) {
            string relativePath = Path.Combine(AppSettings.ImagePath, image.FileName);
            string newName = FileUtils.SaveFile(image, Path.Combine(AppSettings.FolderPath, relativePath));
            return Path.Combine(AppSettings.ImagePath, newName);
        }
    }
}
