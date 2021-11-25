using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.service;

namespace WebAPI.controller {
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase {

        private IUploadService uploadService;

        public UploadController(IUploadService uploadService) {
            this.uploadService = uploadService;
        }

        [HttpPost("image")]
        public string SaveImage(IFormFile image) {
            return uploadService.SaveImage(image);
        }
    }
}
