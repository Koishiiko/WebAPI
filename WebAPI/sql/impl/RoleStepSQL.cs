using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class RoleStepSQL : IRoleStepSQL {

		private IDataSource dataSource { get; }

		public RoleStepSQL(IDataSource dataSource) {
			this.dataSource = dataSource;
		}

		public int SaveSteps(long roleId, List<int> ids) {
			List<RoleStep> roleSteps = new List<RoleStep>();

			ids.ForEach((stepId) => {
				roleSteps.Add(new RoleStep() { RoleId = (int)roleId, StepId = stepId });
			});

			return dataSource.Save(roleSteps);
		}

		public int DeleteByRoleId(int id) {
			string sql = @"
				DELETE FROM role_step WHERE role_id = @id
			";

			return dataSource.Delete(sql, new { id });
		}
	}
}
