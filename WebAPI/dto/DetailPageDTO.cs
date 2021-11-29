using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.dto {
	public class DetailPageDTO {

		public List<DetailDTO> Data { get; set; }
		public int Total { get; set; }
	}
}
