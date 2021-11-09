using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class ReportTemplateSQL : IReportTemplateSQL {

		private IDataSource dataSource { get; }

		public ReportTemplateSQL(IDataSource dataSource) {
			this.dataSource = dataSource;
		}

		public List<ReportTemplate> GetAll() {
			string sql = @"
				SELECT id, name, path FROM report_template
			";
			return dataSource.QueryMany<ReportTemplate>(sql);
		}

		public ReportTemplate GetById(int id) {
			string sql = @"
				SELECT id, name, path FROM report_template WHERE id = @id
			";
			return dataSource.QueryOne<ReportTemplate>(sql, new { id });
		}

		public long Save(ReportTemplate reportTemplate) {
			return dataSource.Save(reportTemplate);
		}
	}
}
