using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.dto;
using WebAPI.pagination;
using WebAPI.service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.controller {
    [Route("[controller]")]
	[ApiController]
	public class ReportsController : ControllerBase {

		private readonly IReportService reportService;

		public ReportsController(IReportService reportService) {
			this.reportService = reportService;
		}

		[HttpPost("page")]
		public ReportPageDTO GetByPage([FromBody] ReportPagination pagination) {
			return reportService.GetByPage(pagination);
		}

		[HttpGet]
		public List<ReportDTO> GetByProductId([FromQuery] string productId) {
			return reportService.GetByProductId(productId);
		}
	}
}
