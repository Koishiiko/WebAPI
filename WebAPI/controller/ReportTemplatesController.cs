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
	public class ReportTemplatesController : ControllerBase {

		private IReportTemplateService reportTemplateService { get; }

		public ReportTemplatesController(IReportTemplateService reportTemplateService) {
			this.reportTemplateService = reportTemplateService;
		}

		[HttpGet]
		public List<ReportTemplate> GetAll() {
			return reportTemplateService.GetAll();
		}
	}
}
