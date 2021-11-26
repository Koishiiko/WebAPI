using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.po;
using WebAPI.pagination;
using WebAPI.dto;

namespace WebAPI.service {
	public interface IReportService {
		ReportPageDTO GetByPage(ReportPagination pagination);

		List<ReportDTO> GetByProductId(string productId);
	}
}
