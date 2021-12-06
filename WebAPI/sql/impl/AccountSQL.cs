using System.Collections.Generic;
using WebAPI.pagination;
using WebAPI.entity;
using WebAPI.utils;
using WebAPI.po;

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

        public List<AccountPagePO> GetByPage(AccountPagination pagination) {
            string sql = @"
                SELECT
                    a.id, a.account_key, a.account_name, r.id AS role_id, r.name AS role_name
                FROM (
                        SELECT
	                        TOP (@end) ROW_NUMBER() OVER(ORDER BY id) row_num,
	                        id, account_key, account_name
                        FROM
	                        account
                        WHERE
                            @accountKey is NULL OR
                            @accountKey = '' OR
                            account_key = @accountKey
                    ) a
                    LEFT JOIN
	                    account_role ar
                    ON a.id = ar.account_id
                    LEFT JOIN
	                    role r
                    ON r.id = ar.role_id
                WHERE
                    a.row_num > @start
                AND (
                    @roleId IS NULL OR
                    @roleId = '' OR
                    ar.role_id = @roleId
                )
                ORDER BY a.id
			";
            return DataSource.QueryMany<AccountPagePO>(sql, new {
                start = pagination.Page * pagination.Size,
                end = (pagination.Page + 1) * pagination.Size,
                accountKey = pagination.AccountKey,
                roleId = pagination.RoleId
            });
        }

        public int GetCount(AccountPagination pagination) {
            string sql = @"
               SELECT
	               COUNT(a.id) AS rows
               FROM (
	               SELECT
		               a0.id
	               FROM account a0
		               LEFT JOIN
			               account_role ar
		               ON a0.id = ar.account_id
                       WHERE (
                           @accountKey IS NULL OR
                           @accountKey = '' OR
                           a0.account_key = @accountKey
                       ) AND (
                           @roleId IS NULL OR
                           @roleId = '' OR
                           ar.role_id = @roleId
                       )
	               GROUP BY a0.id
               ) a
			";
            return DataSource.QueryOne<int>(sql, new {
                accountKey = pagination.AccountKey,
                roleId = pagination.RoleId
            });
        }

        public List<AccountDataPO> GetDataByAccountKey(string accountKey) {
            return DataSource.DB.Queryable<Account>().Where(a => a.AccountKey == accountKey)
                .LeftJoin<AccountRole>((a, ar) => a.Id == ar.AccountId)
                .Select((a, ar) => new AccountDataPO { Id = a.Id, AccountKey = a.AccountKey, AccountName = a.AccountName, Password = a.Password, RoleId = ar.RoleId })
                .ToList();
        }

        public List<AccountDataPO> GetDataById(int id) {
            return DataSource.DB.Queryable<Account>()
                .LeftJoin<AccountRole>((a, ar)=> a.Id == ar.AccountId)
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
