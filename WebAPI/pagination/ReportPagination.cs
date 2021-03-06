using System;

namespace WebAPI.pagination {
    public class ReportPagination {

		public int Page { get; set; }
		public int Size { get; set; }
		public string ProductId { get; set; }
		public DateTime? BeginTime { get; set; }
		public DateTime? EndTime { get; set; }
	}
}
