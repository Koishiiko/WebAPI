using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.sql {
	public interface IRoleStepSQL {
		int SaveSteps(long roleId, List<int> ids);

		int DeleteByRoleId(int id);

		int DeleteByStepId(int id);
	}
}
