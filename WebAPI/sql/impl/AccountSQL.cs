using System.Collections.Generic;
using WebAPI.pagination;
using WebAPI.entity;
using WebAPI.utils;
using WebAPI.po;
using SqlSugar;

namespace WebAPI.sql.impl {
    public class AccountSQL : IAccountSQL {

        public List<Account> GetAll() {
            return DataSource.DB.Queryable<Account>().ToList();
        }

        public Account GetByAccountKey(string accountKey) {
            return DataSource.DB.Queryable<Account>().Single(a => a.AccountKey == accountKey);
        }

        public Account GetById(int id) {
            return DataSource.DB.Queryable<Account>().InSingle(id);
        }

        public List<AccountPagePO> GetByPage(AccountPagination pagination, out int total) {
            total = 0;
            return DataSource.DB.Queryable<Account, AccountRole, Role>((a, ar, r) =>
                new JoinQueryInfos(JoinType.Left, a.Id == ar.AccountId, JoinType.Left, ar.RoleId == r.Id))
                .Where((a, ar, r) => (a.AccountKey == pagination.AccountKey || string.IsNullOrEmpty(pagination.AccountKey)) &&
                                        (ar.RoleId == pagination.RoleId || pagination.RoleId == 0))
                .Select((a, ar, r) => new AccountPagePO { Id = a.Id, AccountKey = a.AccountKey, AccountName = a.AccountName, RoleId = r.Id, RoleName = r.Name })
                .ToPageList(pagination.Page, pagination.Size, ref total);
        }

        public List<AccountDataPO> GetDataByAccountKey(string accountKey) {
            return DataSource.DB.Queryable<Account>().Where(a => a.AccountKey == accountKey)
                .LeftJoin<AccountRole>((a, ar) => a.Id == ar.AccountId)
                .Select((a, ar) => new AccountDataPO { Id = a.Id, AccountKey = a.AccountKey, AccountName = a.AccountName, Password = a.Password, RoleId = ar.RoleId })
                .ToList();
        }

        public List<AccountDataPO> GetDataById(int id) {
            return DataSource.DB.Queryable<Account>()
                .LeftJoin<AccountRole>((a, ar) => a.Id == ar.AccountId)
                .Where(a => a.Id == id)
                .Select((a, ar) => new AccountDataPO { Id = a.Id, AccountKey = a.AccountKey, AccountName = a.AccountName, RoleId = ar.RoleId })
                .ToList();
        }

        public long Save(Account account) {
            return DataSource.Save(account);
        }

        public bool Update(Account account) {
            return DataSource.Update(account);
        }

        public int Delete(int id) {
            return DataSource.DB.Deleteable<Account>().In(id).ExecuteCommand();
        }
    }
}
