using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.service;
using WebAPI.attribute;
using NPOI.SS.UserModel;
using WebAPI.utils;

namespace WebAPI.controller {
	[Route("[controller]")]
	[ApiController]
	public class ReportTemplatesController : ControllerBase {

		private readonly IReportTemplateService reportTemplateService;

		public ReportTemplatesController(IReportTemplateService reportTemplateService) {
			this.reportTemplateService = reportTemplateService;
		}

		[HttpGet]
		public List<ReportTemplate> GetAll() {
			return reportTemplateService.GetAll();
		}

		[HttpPost]
		public string Save(IFormFile file) {
			return reportTemplateService.Save(file);
		}

		[HttpGet("{productId}/{templateId}")]
		[UnpackageResult]
		public IActionResult GetTemplate(string productId, int templateId) {
			IWorkbook workbook = reportTemplateService.GetTemplate(productId, templateId, out string name);

			var ms = new MemoryStream();
			workbook.Write(ms);
			ms.Position = 0;

			return File(ms, "application/octet-stream", name);
		}
	}
}
