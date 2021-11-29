using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;
using WebAPI.pagination;

namespace WebAPI.sql {
	public interface IReportSQL {

		List<Report> GetAll();

		List<ReportDTO> GetByPage(ReportPagination pagination);

		int GetCount(ReportPagination pagination);

		List<ReportDTO> GetByProductId(string productId);

		List<ReportDTO> GetALLByProductId(string productId);

		Report GetLastByProductId(int stepId, string productId);

		Report GetByGuid(string guid);

		long Save(Report report);
	}
}
