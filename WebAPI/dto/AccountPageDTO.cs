using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.dto {
	public class AccountPageDTO {

		public List<AccountDTO> Data { get; set; }
		public int Total;
	}
}
