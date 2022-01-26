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
using NPOI.SS.UserModel;
using WebAPI.utils;

namespace WebAPI.controller {
	[Route("api/[controller]")]
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

		/// <summary>
		/// 保存报告模板
		/// 保存到配置文件中设置的目录下
		/// </summary>
		/// <param name="file"></param>
		/// <returns>文件的相对路径(template/filename.xlsx)</returns>
		[HttpPost]
		public string Save(IFormFile file) {
			return reportTemplateService.Save(file);
		}

		[HttpGet("export")]
		public IActionResult GetTemplate([FromQuery] string productId, [FromQuery] int templateId) {
			IWorkbook workbook = reportTemplateService.GetTemplate(productId, templateId, out string name);

			var ms = new MemoryStream();
			workbook.Write(ms);
			ms.Position = 0;

			return File(ms, "application/octet-stream", name);
		}
	}
}
