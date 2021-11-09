using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.pagination;
using WebAPI.sql;

namespace WebAPI.service.impl {
	public class AccountService : IAccountService {

		private IAccountSQL accountSQL { get; set; }
		private IAccountRoleSQL accountRoleSQL { get; set; }

		public AccountService(IAccountSQL accountSQL, IAccountRoleSQL accountRoleSQL) {
			this.accountSQL = accountSQL;
			this.accountRoleSQL = accountRoleSQL;
		}

		public string Login(Account account) {
			throw new NotImplementedException();
		}

		public string LoginByToken(string token) {
			throw new NotImplementedException();
		}

		public List<Account> GetAll() {
			return accountSQL.GetAll();
		}

		public Account GetByAccountKey(string accountKey) {
			return accountSQL.GetByAccountKey(accountKey);
		}

		public AccountDTO GetDataByAccountKey(string accountKey) {
			List<dynamic> rows = accountSQL.GetDataByAccountKey(accountKey);
			if (!rows.Any()) {
				return null;
			}
			return this.GetData(rows);
		}

		public AccountPageDTO GetByPage(AccountPagination pagination) {
			return new AccountPageDTO() {
				Data = this.GetData(pagination),
				Total = accountSQL.GetCount(pagination)
			};
		}

		private List<AccountDTO> GetData(AccountPagination pagination) {
			List<dynamic> rows = accountSQL.GetByPage(pagination);
			List<AccountDTO> accounts = new List<AccountDTO>();
			AccountDTO curr = null;
			rows.ForEach((row) => {
				if (curr == null || curr.Id != row.id) {
					curr = new AccountDTO() {
						Id = row.id,
						AccountKey = row.account_key,
						AccountName = row.account_name,
						Roles = new List<Role>()
					};
				}
				if (row.role_id != null) {
					curr.Roles.Add(
						new Role() {
							Id = row.role_id,
							Name = row.role_name
						});
				}
				if (!accounts.Any() || accounts.Last().Id != curr.Id) {
					accounts.Add(curr);
				}
			});
			return accounts;
		}

		public Account GetById(int id) {
			return accountSQL.GetById(id);
		}

		public AccountDTO GetDataById(int id) {
			List<dynamic> rows = accountSQL.GetDataById(id);
			if (!rows.Any()) {
				return null;
			}
			return this.GetData(rows);
		}

		private AccountDTO GetData(List<dynamic> rows) {
			AccountDTO data = new AccountDTO() {
				Id = rows[0].id,
				AccountKey = rows[0].account_key,
				AccountName = rows[0].account_name,
				Roles = new List<Role>()
			};

			if (rows[0].role_id != null) {
				rows.ForEach((row) => {
					data.Roles.Add(new Role() {
						Id = row.role_id
					});
				});
			}
			return data;
		}

		public long Save(AccountDTO account) {
			Account data = new Account() {
				AccountKey = account.AccountKey,
				AccountName = account.AccountName
			};

			long id = accountSQL.Save(data);

			if (account.Roles.Any()) {
				List<int> ids = new List<int>();
				account.Roles.ForEach((role) => {
					ids.Add(role.Id);
				});
				accountRoleSQL.SaveRoles(account.Id, ids);
			}

			return id;
		}

		public bool Update(AccountDTO account) {
			Account data = new Account() {
				Id = account.Id,
				AccountKey = account.AccountKey,
				AccountName = account.AccountName
			};

			bool res = accountSQL.Update(data);

			accountRoleSQL.DeleteByAccountId(account.Id);
			if (account.Roles.Any()) {
				List<int> ids = new List<int>();
				account.Roles.ForEach((role) => {
					ids.Add(role.Id);
				});
				accountRoleSQL.SaveRoles(account.Id, ids);
			}

			return res;
		}

		public int Delete(int id) {
			int count = accountSQL.Delete(id);
			accountRoleSQL.DeleteByAccountId(id);
			return count;
		}
	}
}
