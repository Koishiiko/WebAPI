using System.Collections.Generic;
using WebAPI.dto;
using WebAPI.pagination;

namespace WebAPI.service {
    public interface IReportService {

		ReportPageDTO GetByPage(ReportPagination pagination);

		List<ReportDTO> GetByProductId(string productId);
	}
}
