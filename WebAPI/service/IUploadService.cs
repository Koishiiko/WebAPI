using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebAPI.service {
    public interface IUploadService {

        string SaveImage(IFormFile image);
    }
}
