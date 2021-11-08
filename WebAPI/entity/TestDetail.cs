using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace WebAPI.entity {
	public class TestDetail {
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string TestGuid { get; set; }
		public string ProductId { get; set; }
		public int StepId { get; set; }
		public int StationId { get; set; }
		public string ModuleKey { get; set; }
		public string ItemKey { get; set; }
		public string RecordKey { get; set; }
		public string RecordValue { get; set; }
	}
}
