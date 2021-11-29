using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.entity;
using WebAPI.service;

namespace WebAPI.controller {

    [Route("[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase {

        private readonly IModuleService moduleService;

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

        [HttpPost]
        public long Save([FromBody] Module module) {
            return module.Id == 0 ? moduleService.Save(module) : moduleService.Update(module);
        }

        [HttpDelete("{id}")]
        public int Delete(string id) {
            return moduleService.Delete(id);
        }
    }
}
