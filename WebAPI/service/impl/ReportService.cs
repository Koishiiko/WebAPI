using System.Collections.Generic;
using WebAPI.dto;
using WebAPI.pagination;
using WebAPI.sql;

namespace WebAPI.service.impl {
    public class ReportService : IReportService {

		private readonly IReportSQL reportSQL;

		public ReportService(IReportSQL reportSQL) {
			this.reportSQL = reportSQL;
		}

		public ReportPageDTO GetByPage(ReportPagination pagination) {
			return new ReportPageDTO() {
				Data = reportSQL.GetByPage(pagination),
				Total = reportSQL.GetCount(pagination)
			};
		}

		public List<ReportDTO> GetByProductId(string productId) {
			return reportSQL.GetByProductId(productId);
		}
	}
}
