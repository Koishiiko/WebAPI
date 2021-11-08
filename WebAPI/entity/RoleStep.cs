using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace WebAPI.entity {
	[SugarTable("role_step")]
	public class RoleStep {
		[SugarColumn(IsPrimaryKey =true, IsIdentity =true)]
		public int Id { get; set; }
		public int RoleId { get; set; }
		public int StepId { get; set; }
	}
}
