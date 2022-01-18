using Microsoft.AspNetCore.Mvc;
using WebAPI.service;
using WebAPI.dto;
using WebAPI.utils;

namespace WebAPI.controller {
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase {

        private readonly IMaterialService materialService;

        public MaterialsController(IMaterialService materialService) {
            this.materialService = materialService;
        }

        [HttpGet("{id}")]
        public MaterialDTO GetByProductId(string id) {
            return materialService.GetByProductId(id);
        }

        /// <summary>
        /// 根据工序Id和产品Id获取原材料信息
        /// 包含当前工序以及产品基本信息(stepId == 0)
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("step/{stepId}/{productId}")]
        public MaterialDTO GetByStep(int stepId, string productId) {
            return materialService.GetStepDataByProductId(stepId, productId);
        }

        /// <summary>
        /// 根据guid获取原材料信息
        /// 包括当前guid对应的信息 以及产品基本信息(stepId == 0)
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet("guid/{guid}")]
        public MaterialDTO GetByGuid(string guid) {
            return materialService.GetByGuid(guid);
        }

        [HttpPost]
        public Result Save([FromBody] MaterialDTO material, [FromHeader] string authorization) {
            materialService.Save(material, JWTUtils.Decode<AccountPayload>(authorization));
            return Result.Success();
        }
    }
}
