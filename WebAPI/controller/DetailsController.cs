using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.service;
using WebAPI.dto;
using WebAPI.pagination;

namespace WebAPI.controller {
	[Route("[controller]")]
	[ApiController]
	public class DetailsController : ControllerBase {

		private IDetailService detailService { get; }

		public DetailsController(IDetailService detailService) {
			this.detailService = detailService;
		}

		[HttpPost("page")]
		public DetailPageDTO GetPageByGuid([FromBody] DetailPagination pagination) {
			return detailService.GetPageByGuid(pagination);
		}
	}
}
