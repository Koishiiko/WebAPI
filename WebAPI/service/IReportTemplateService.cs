using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using Spire.Xls;
using WebAPI.entity;

namespace WebAPI.service {
	public interface IReportTemplateService {

		List<ReportTemplate> GetAll();

		string Save(IFormFile file);

		Workbook GetTemplate(string productId, int templateId, out string name);

		void PrintTemplate(string productId, int templateId);
	}
}
