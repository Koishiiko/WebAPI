using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace WebAPI.entity {
	[SugarTable("account")]
	public class Account {
		[SugarColumn(IsPrimaryKey =true, IsIdentity =true)]
		public int Id { get; set; }
		public string AccountKey { get; set; }
		public string AccountName { get; set; }
		public string Password { get; set; }
	}
}
