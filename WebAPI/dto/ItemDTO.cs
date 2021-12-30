using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;

namespace WebAPI.dto {
	public class ItemDTO {

		public int Id { get; set; }
		public string ModuleId { get; set; }
		public string ItemId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string RecordId { get; set; }
		public string RecordName { get; set; }
		public string ReportId { get; set; }
		public ItemRule Rules { get; set; }
		public List<Suggestion> Suggestions { get; set; }
	}
}
