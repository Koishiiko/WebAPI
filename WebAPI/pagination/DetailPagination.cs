using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.pagination {
	public class DetailPagination {
		public int Page { get; set; }
		public int Size { get; set; }
		public string ModuleId { get; set; }
		public string Guid { get; set; }
	}
}
