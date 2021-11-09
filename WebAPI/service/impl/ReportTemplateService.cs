using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.sql;

namespace WebAPI.service.impl {
	public class ReportTemplateService : IReportTemplateService {

		private IReportTemplateSQL reportTemplateSQL { get; }

		public ReportTemplateService(IReportTemplateSQL reportTemplateSQL) {
			this.reportTemplateSQL = reportTemplateSQL;
		}

		public List<ReportTemplate> GetAll() {
			return reportTemplateSQL.GetAll();
		}
	}
}
