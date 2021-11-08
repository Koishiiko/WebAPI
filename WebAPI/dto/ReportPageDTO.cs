using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.dto {
	public class ReportPageDTO {
		public List<ReportDTO> Data { get; set; }
		public int Total { get; set; }
	}
}
