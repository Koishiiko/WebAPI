using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.service;

namespace WebAPI.controller {

	[Route("[controller]")]
	[ApiController]
	public class ModulesController : ControllerBase {

		private IModuleService moduleService { get; }

		public ModulesController(IModuleService moduleService) {
			this.moduleService = moduleService;
		}

		[HttpGet]
		public List<Module> GetByStepId([FromQuery] int stepId) {
			return moduleService.GetByStepId(stepId);
		}

		[HttpGet("{id}")]
		public Module GetById(string id) {
			return moduleService.GetById(id);
		}

		[HttpPut]
		public bool Update(Module module) {
			return moduleService.Update(module);
		}

		[HttpPost]
		public long Save(Module module) {
			return moduleService.Save(module);
		}

		[HttpDelete("{id}")]
		public int Delete(int id) {
			return moduleService.Delete(id);
		}
	}
}
