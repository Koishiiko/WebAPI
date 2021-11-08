using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.utils;
using WebAPI.entity;

namespace WebAPI.sql.impl {
	public class AccountRoleSQL : IAccountRoleSQL {

		private IDataSource dataSource { get; }

		public AccountRoleSQL(IDataSource dataSource) {
			this.dataSource = dataSource;
		}

		public int SaveAccounts(long roleId, List<int> ids) {
			List<AccountRole> accountRoles = new List<AccountRole>();
			ids.ForEach((accountId) => {
				accountRoles.Add(new AccountRole() { RoleId = (int)roleId, AccountId = accountId });
			});
			return dataSource.Save(accountRoles);
		}

		public int SaveRoles(long accountId, List<int> ids) {
			List<AccountRole> accountRoles = new List<AccountRole>();
			ids.ForEach((roleId) => {
				accountRoles.Add(new AccountRole() { AccountId = (int)accountId, RoleId = roleId });
			});
			return dataSource.Save(accountRoles);
		}

		public int DeleteByRoleId(int id) {
			string sql = @"
                DELETE FROM account_role WHERE role_id = @id
			";
			return dataSource.Delete(sql, new { id });
		}


		public int DeleteByAccountId(int id) {
			string sql = @"
				DELETE FROM account_role WHERE account_id = @id;
			";
			return dataSource.Delete(sql, new { id });
		}
	}
}
