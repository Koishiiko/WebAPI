using Microsoft.AspNetCore.Mvc;
using WebAPI.dto;
using WebAPI.pagination;
using WebAPI.service;

namespace WebAPI.controller {
    [Route("api/[controller]")]
	[ApiController]
	public class DetailsController : ControllerBase {

		private readonly IDetailService detailService;

		public DetailsController(IDetailService detailService) {
			this.detailService = detailService;
		}

		[HttpGet("page")]
		public DetailPageDTO GetPageByGuid([FromQuery] DetailPagination pagination) {
			return detailService.GetPageByGuid(pagination);
		}
	}
}
