using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;

namespace WebAPI.sql {
	public interface IReportTemplateSQL {

		List<ReportTemplate> GetAll();

		ReportTemplate GetById(int id);

		long Save(ReportTemplate reportTemplate);
	}
}
