using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.service;
using WebAPI.entity;
using WebAPI.dto;
using WebAPI.filter;

namespace WebAPI.controller {
	[Route("[controller]")]
	[ApiController]
	public class MaterialsController : ControllerBase {

		private IMaterialService materialService { get; }

		public MaterialsController(IMaterialService materialService) {
			this.materialService = materialService;
		}

		[HttpGet("productId/{id}")]
		public MaterialDTO GetByProductId(string id) {
			return materialService.GetByProductId(id);
		}

		[HttpGet("step/{stepId}/{productId}")]
		public MaterialDTO GetByProductId(int stepId, string productId) {
			return materialService.GetStepDataByProductId(stepId, productId);
		}

		[HttpGet("guid/{guid}")]
		public MaterialDTO GetByGuid(string guid) {
			return materialService.GetByGuid(guid);
		}

		[HttpPost]
		public bool Save([FromBody] MaterialDTO material) {
			return materialService.Save(material);
		}

	}
}
