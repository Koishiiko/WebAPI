using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace WebAPI.entity {
	[Table("suggestion")]
	public class Suggestion {
		public Suggestion() {
		}

		public Suggestion(int id, string moduleId, string itemId, string value) {
			Id = id;
			Module_id = moduleId;
			Item_id = itemId;
			Value = value;
		}

		[Key]
		public int Id { get; set; }
		public string Module_id { get; set; }
		public string Item_id { get; set; }
		public string Value { get; set; }
	}
}
