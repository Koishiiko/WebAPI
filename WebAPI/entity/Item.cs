using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using DapperExtensions.Mapper;

namespace WebAPI.entity {
	[Table("item")]
	public class Item {
		public Item() {
		}

		public Item(int id, string moduleId, string itemId, string name, string type, string recordId, string reportId) {
			Id = id;
			Module_id = moduleId;
			Item_id = itemId;
			Name = name;
			Type = type;
			Record_id = recordId;
			Report_id = reportId;
		}

		[Key]
		public int Id { get; set; }
		public string Module_id { get; set; }
		public string Item_id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Record_id { get; set; }
		public string Report_id { get; set; }
	}

}
