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
