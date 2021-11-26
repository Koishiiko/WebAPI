using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.po;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class RoleStepSQL : IRoleStepSQL {

		public int SaveSteps(long roleId, List<int> ids) {
			List<RoleStep> roleSteps = new List<RoleStep>();

			ids.ForEach((stepId) => {
				roleSteps.Add(new RoleStep() { RoleId = (int)roleId, StepId = stepId });
			});

			return DataSource.Save(roleSteps);
		}

		public int DeleteByRoleId(int id) {
			string sql = @"
				DELETE FROM role_step WHERE role_id = @id
			";

			return DataSource.Delete(sql, new { id });
		}

		public int DeleteByStepId(int id) {
			string sql = @"
				DELETE FROM role_step WHERE step_id = @id
			";

			return DataSource.Delete(sql, new { id });
		}
	}
}
