using System.Collections.Generic;

namespace WebAPI.dto {
    public class ReportPageDTO {

		public List<ReportDTO> Data { get; set; }
		public int Total { get; set; }
	}
}
