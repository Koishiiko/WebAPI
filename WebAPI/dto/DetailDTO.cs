using System.Collections.Generic;

namespace WebAPI.dto {
    public class DetailDTO {

		public string ModuleId { get; set; }
		public string ModuleName { get; set; }
		public List<DetailItem> Items { get; set; }
	}

	public class DetailItem {

		public string ReportId { get; set; }
		public string ItemName { get; set; }
		public int Type { get; set; }
		public bool Required { get; set; }
		public double MinValue { get; set; }
		public double MaxValue { get; set; }
		public int? MinLength { get; set; }
		public int? MaxLength { get; set; }
		public string Value { get; set; }
	}
}
