using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.service;
using WebAPI.dto;

namespace WebAPI.controller {

	[Route("[controller]")]
	[ApiController]
	public class StepsController : ControllerBase {

		private IStepService stepService { get; }

		public StepsController(IStepService stepService) {
			this.stepService = stepService;
		}

		[HttpGet("all")]
		public List<Step> GetAll() {
			return stepService.GetAll();
		}

		[HttpGet]
		public List<Step> GetSteps() {
			return stepService.GetSteps();
		}

		[HttpGet("data")]
		public List<StepDTO> GetStepDatas() {
			return stepService.GetStepDatas();
		}

		[HttpGet("{id}")]
		public Step GetById(int id) {
			return stepService.GetById(id);
		}

		[HttpPut]
		public bool Update(Step step) {
			return stepService.Update(step);
		}

		[HttpPost]
		public long Save(Step step) {
			return stepService.Save(step);
		}

		[HttpDelete("{id}")]
		public int Delete(int id) {
			return stepService.Delete(id);
		}
	}
}
