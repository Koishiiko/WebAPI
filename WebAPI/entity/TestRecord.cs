﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace WebAPI.entity {
	[Table("test_record")]
	public class TestRecord {
		[Key]
		public int Id { get; set; }
		public string Test_guid { get; set; }
		public string Product_id { get; set; }
		public string Product_type { get; set; }
		public DateTime Begin_time { get; set; }
		public DateTime End_time { get; set; }
		public string Testor { get; set; }
		public string Test_result { get; set; }
		public string Upload_flag { get; set; }
	}
}
