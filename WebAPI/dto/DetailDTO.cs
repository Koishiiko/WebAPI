using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.dto {
	public class DetailDTO {

		public string ModuleId { get; set; }
		public string ModuleName { get; set; }
		public List<DetailItem> Items { get; set; }
	}

	public class DetailItem {

		public string ReportId { get; set; }
		public string ItemName { get; set; }
		public string Value { get; set; }
	}
}
