using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace WebAPI.entity {
	public class Suggestion {
		public Suggestion() {
		}

		public Suggestion(int id, string moduleId, string itemId, string value) {
			Id = id;
			ModuleId = moduleId;
			ItemId = itemId;
			Value = value;
		}

		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string ModuleId { get; set; }
		public string ItemId { get; set; }
		public string Value { get; set; }
	}
}
