using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.po;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class ReportTemplateSQL : IReportTemplateSQL {


		public List<ReportTemplate> GetAll() {
			string sql = @"
				SELECT id, name, path FROM report_template
			";
			return DataSource.QueryMany<ReportTemplate>(sql);
		}

		public ReportTemplate GetById(int id) {
			string sql = @"
				SELECT id, name, path FROM report_template WHERE id = @id
			";
			return DataSource.QueryOne<ReportTemplate>(sql, new { id });
		}

		public long Save(ReportTemplate reportTemplate) {
			return DataSource.Save(reportTemplate);
		}
	}
}
