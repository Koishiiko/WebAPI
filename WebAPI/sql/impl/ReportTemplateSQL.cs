using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class ReportTemplateSQL : IReportTemplateSQL {

		public List<ReportTemplate> GetAll() {
			return DataSource.DB.Queryable<ReportTemplate>().ToList();
		}

		public ReportTemplate GetById(int id) {
			return DataSource.DB.Queryable<ReportTemplate>().InSingle(id);
		}

		public long Save(ReportTemplate reportTemplate) {
			return DataSource.Save(reportTemplate);
		}
	}
}
