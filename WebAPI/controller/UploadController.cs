using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.service;

namespace WebAPI.controller {
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase {

        private readonly IUploadService uploadService;

        public UploadController(IUploadService uploadService) {
            this.uploadService = uploadService;
        }

        /// <summary>
        /// 保存图片到配置文件中设置的目录
        /// </summary>
        /// <param name="image"></param>
        /// <returns>图片相对路径(image/photo.jpg)</returns>
        [HttpPost("image")]
        public string SaveImage(IFormFile image) {
            return uploadService.SaveImage(image);
        }
    }
}
