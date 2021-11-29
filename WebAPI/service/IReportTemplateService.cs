using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using WebAPI.entity;

namespace WebAPI.service {
	public interface IReportTemplateService {

		List<ReportTemplate> GetAll();

		string Save(IFormFile file);

		IWorkbook GetTemplate(string productId, int templateId, out string name);
	}
}
