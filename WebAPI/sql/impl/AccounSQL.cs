using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.pagination;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class AccounSQL : IAccountSQL {

		private IDataSource dataSource { get; }

		public AccounSQL(IDataSource dataSource) {
			this.dataSource = dataSource;
		}

		public List<Account> GetAll() {
			string sql = @"
				SELECT id, account_key, account_name FROM account ORDER BY id
			";
			return dataSource.QueryMany<Account>(sql);
		}

		public Account GetByAccountKey(string accountKey) {
			string sql = @"
                SELECT id, account_key, account_name, password FROM account WHERE account_key = @accountKey
			";
			return dataSource.QueryOne<Account>(sql, accountKey);
		}

		public Account GetById(int id) {
			string sql = @"
                SELECT id, account_key, account_name, password FROM account WHERE id = @id
			";
			return dataSource.QueryOne<Account>(sql, id);
		}

		public List<dynamic> GetByPage(AccountPagination pagination) {
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
			return dataSource.QueryMany<dynamic>(sql, new {
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
                           @accounKey = '' OR
                           a0.account_key = @accountKey
                       ) AND (
                           @roleId IS NULL OR
                           @roleId = '' OR
                           ar.role_id = @roleId
                       )
	               GROUP BY a0.id
               ) a
			";
			return dataSource.QueryOne<int>(sql, new {
				accountKey = pagination.AccountKey,
				roleId = pagination.RoleId
			});
		}

		public List<dynamic> GetDataByAccountKey(string accountKey) {
			string sql = @"
                SELECT
	                a.id, a.account_key, a.account_name, a.password, ar.role_id
                FROM (
	                    SELECT
		                    id, account_key, account_name, password
	                    FROM
		                    account
	                    WHERE
		                    account_key = @accountKey
                    ) a
	                LEFT JOIN
		                account_role ar
	                ON a.id = ar.account_id
			";
			return dataSource.QueryMany<dynamic>(sql, new { accountKey });
		}

		public List<dynamic> GetDataById(int id) {
			string sql = @"
                SELECT
	                a.id, a.account_key, a.account_name, ar.role_id
                FROM (
	                SELECT
		                id, account_key, account_name
	                FROM
		                account
	                WHERE
		                id = @id
                ) a
	                LEFT JOIN
		                account_role ar
	                ON a.id = ar.account_id
			";
			return dataSource.QueryMany<dynamic>(sql, new { id });
		}

		public long Save(Account account) {
			return dataSource.Save(account);
		}

		public bool Update(Account account) {
			return dataSource.Update(account);
		}

		public int Delete(int id) {
			string sql = @"
				DELETE FROM account WHERE id = @id
			";
			return dataSource.Delete(sql, new { id });
		}
	}
}
