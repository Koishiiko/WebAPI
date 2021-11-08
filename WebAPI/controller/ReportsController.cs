using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.service;
using WebAPI.dto;
using WebAPI.pagination;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.controller {
	[Route("[controller]")]
	[ApiController]
	public class ReportsController : ControllerBase {

		private IReportService reportService { get; }

		public ReportsController(IReportService reportService) {
			this.reportService = reportService;
		}

		[HttpGet("page")]
		public ReportPageDTO GetByPage([FromBody] ReportPagination pagination) {
			return reportService.GetByPage(pagination);
		}

		[HttpGet]
		public List<ReportDTO> GetByProductId([FromQuery] string productId) {
			return reportService.GetByProductId(productId);
		}
	}
}
