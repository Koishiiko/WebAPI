using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.entity {
	[SugarTable("account_role")]
	public class AccountRole {
		[SugarColumn(IsPrimaryKey =true, IsIdentity =true)]
		public int Id { get; set; }
		public int AccountId { get; set; }
		public int RoleId { get; set; }
	}
}
