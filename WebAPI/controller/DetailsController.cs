using Microsoft.AspNetCore.Mvc;
using WebAPI.dto;
using WebAPI.service;
using System.Collections.Generic;

namespace WebAPI.controller {
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase {

        private readonly IDetailService detailService;

        public DetailsController(IDetailService detailService) {
            this.detailService = detailService;
        }

        [HttpGet]
        public IEnumerable<DetailDTO> GetByProductId(string productId) {
            return detailService.GetByProductId(productId);
        }

        [HttpGet("step")]
        public IEnumerable<DetailDTO> GetByGuid(string guid) {
            return detailService.GetByGuid(guid);
        }
    }
}
