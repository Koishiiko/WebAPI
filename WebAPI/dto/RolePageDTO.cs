﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.po;

namespace WebAPI.dto {
	public class RolePageDTO {
		public List<Role> Data { get; set; }
		public int Total;
	}
}
