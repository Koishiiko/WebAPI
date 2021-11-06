using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace WebAPI.entity {
	[Table("test_detail")]
	public class TestDetail {
		[Key]
		public int Id { get; set; }
		public string Test_guid { get; set; }
		public string Product_id { get; set; }
		public int Step_id { get; set; }
		public int Station_id { get; set; }
		public string Module_key { get; set; }
		public string Item_key { get; set; }
		public string Record_key { get; set; }
		public string Record_value { get; set; }
	}
}
