using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace WebAPI.entity {
	[Table("account")]
	public class Account {
		[Key]
		public int Id { get; set; }
		public string Account_key { get; set; }
		public string Account_name { get; set; }
		public string Password { get; set; }
	}
}
