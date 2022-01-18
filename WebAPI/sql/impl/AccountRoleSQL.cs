using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.utils;
using WebAPI.entity;

namespace WebAPI.sql.impl {
    public class AccountRoleSQL : IAccountRoleSQL {

        public int SaveAccounts(long roleId, List<int> ids) {
            List<AccountRole> accountRoles = new List<AccountRole>();
            ids.ForEach((accountId) => {
                accountRoles.Add(new AccountRole() { RoleId = (int)roleId, AccountId = accountId });
            });
            return DataSource.Save(accountRoles);
        }

        public int SaveRoles(long accountId, List<int> ids) {
            List<AccountRole> accountRoles = new List<AccountRole>();
            ids.ForEach((roleId) => {
                accountRoles.Add(new AccountRole() { AccountId = (int)accountId, RoleId = roleId });
            });
            return DataSource.Save(accountRoles);
        }

        public int DeleteByRoleId(int id) {
            return DataSource.Switch.Deleteable<AccountRole>().Where(ar => ar.RoleId == id).ExecuteCommand();
        }

        public int DeleteByAccountId(int id) {
            return DataSource.Switch.Deleteable<AccountRole>().Where(ar => ar.AccountId == id).ExecuteCommand();
        }
    }
}
