using System.Collections.Generic;
using WebAPI.entity;

namespace WebAPI.dto {
    public class AccountDTO {

		public int Id { get; set; }
		public string AccountKey { get; set; }
		public string AccountName { get; set; }
		public List<Role> Roles { get; set; }
	}
}
