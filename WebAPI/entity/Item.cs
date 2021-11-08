using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace WebAPI.entity {
	public class Item {
		public Item() {
		}

		public Item(int id, string moduleId, string itemId, string name, string type, string recordId, string reportId) {
			Id = id;
			ModuleId = moduleId;
			ItemId = itemId;
			Name = name;
			Type = type;
			RecordId = recordId;
			ReportId = reportId;
		}

		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string ModuleId { get; set; }
		public string ItemId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string RecordId { get; set; }
		public string ReportId { get; set; }
	}

}
