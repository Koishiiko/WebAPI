using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.service;
using WebAPI.dto;
using WebAPI.utils;
using System;

namespace WebAPI.controller {

    [Route("api/[controller]")]
    [ApiController]
    public class StepsController : ControllerBase {

        private readonly IStepService stepService;

        public StepsController(IStepService stepService) {
            this.stepService = stepService;
        }

        [HttpGet("all")]
        public List<Step> GetAll() {
            return stepService.GetAll();
        }

        [HttpGet]
        public List<Step> GetSteps([FromHeader] string authorization) {
            var payload = JWTUtils.Decode<AccountPayload>(authorization);
            return stepService.GetSteps(payload);
        }

        [HttpGet("data")]
        public List<StepDTO> GetStepDatas([FromHeader] string authorization) {
            var payload = JWTUtils.Decode<AccountPayload>(authorization);
            return stepService.GetStepDatas(payload);
        }

        [HttpGet("{id}")]
        public Step GetById(int id) {
            return stepService.GetById(id);
        }

        [HttpPost]
        public long Save(Step step) {
            return step.Id == 0 ? stepService.Save(step) : stepService.Update(step);
        }

        [HttpDelete("{id}")]
        public int Delete(int id) {
            return stepService.Delete(id);
        }
    }
}
