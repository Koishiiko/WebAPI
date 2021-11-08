using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.pagination {
	public class AccountPagination {
		public int Page { get; set; }
		public int Size { get; set; }
		public string AccountKey { get; set; }
		public int RoleId { get; set; }
	}
}
