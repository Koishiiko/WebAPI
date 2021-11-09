﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;

namespace WebAPI.dto {
	public class AccountDTO {
		public int Id { get; set; }
		public string AccountKey { get; set; }
		public string AccountName { get; set; }
		public List<Role> Roles { get; set; }
	}
}
