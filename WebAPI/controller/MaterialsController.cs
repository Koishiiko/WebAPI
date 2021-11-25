using Microsoft.AspNetCore.Mvc;
using WebAPI.service;
using WebAPI.dto;
using WebAPI.utils;

namespace WebAPI.controller {
    [Route("[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase {

        private IMaterialService materialService { get; }

        public MaterialsController(IMaterialService materialService) {
            this.materialService = materialService;
        }

        [HttpGet("{id}")]
        public MaterialDTO GetByProductId(string id) {
            return materialService.GetByProductId(id);
        }

        [HttpGet("step/{stepId}/{productId}")]
        public MaterialDTO GetByStep(int stepId, string productId) {
            return materialService.GetStepDataByProductId(stepId, productId);
        }

        [HttpGet("guid/{guid}")]
        public MaterialDTO GetByGuid(string guid) {
            return materialService.GetByGuid(guid);
        }

        [HttpPost]
        public bool Save([FromBody] MaterialDTO material, [FromHeader] string authorization) {
            return materialService.Save(material, JWTUtils.Decode<AccountJWTPayload>(authorization));
        }
    }
}
