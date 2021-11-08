using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;

namespace WebAPI.dto {
	public class RoleDTO {
		public int Id { get; set; }
		public string Name { get; set; }
		public List<int> AccountIds { get; set; }
		public List<int> StepIds { get; set; }
	}
}
