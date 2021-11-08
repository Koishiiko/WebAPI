using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.sql {
	public interface IAccountRoleSQL {

		int SaveAccounts(long roleId, List<int> ids);

		int SaveRoles(long accountId, List<int> ids);

		int DeleteByRoleId(int id);

		int DeleteByAccountId(int id);
	}
}
