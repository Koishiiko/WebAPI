using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace WebAPI.entity {
	[SugarTable("test_record")]
	public class Report {
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string TestGuid { get; set; }
		public string ProductId { get; set; }
		public string ProductType { get; set; }
		public int StepId { get; set; }
		public int StationId { get; set; }
		public DateTime BeginTime { get; set; }
		public DateTime EndTime { get; set; }
		public string Testor { get; set; }
		public int TestResult { get; set; }
		public string UploadFlag { get; set; }
	}
}
