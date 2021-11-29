using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;

namespace WebAPI.dto {
	public class ReportDTO {
		public int Id { get; set; }
		public string TestGuid { get; set; }
		public string ProductId { get; set; }
		public string ProductType { get; set; }
		public int StepId { get; set; }
		public string StepName { get; set; }
		public int StationId { get; set; }
		public DateTime BeginTime { get; set; }
		public DateTime EndTime { get; set; }
		public string Testor { get; set; }
		public string TestResult { get; set; }
		public string UploadFlag { get; set; }
	}
}
