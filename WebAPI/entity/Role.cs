﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace WebAPI.entity {
	[Table("role")]
	public class Role {
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
