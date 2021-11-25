using System.Collections.Generic;
using System.Linq;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.pagination;
using WebAPI.sql;
using WebAPI.utils;
using WebAPI.exception;

namespace WebAPI.service.impl {
    public class AccountService : IAccountService {

        private IAccountSQL accountSQL { get; }
        private IAccountRoleSQL accountRoleSQL { get; }

        public AccountService(IAccountSQL accountSQL, IAccountRoleSQL accountRoleSQL) {
            this.accountSQL = accountSQL;
            this.accountRoleSQL = accountRoleSQL;
        }

        public string Login(Account account) {
            List<dynamic> rows = accountSQL.GetDataByAccountKey(account.AccountKey);

            if (!rows.Any() || rows[0].password != account.Password) {
                throw new AccountException(ResultCode.ACCOUNT_OR_PASSWORD_ERROR);
            }

            return GetJWT(rows);
        }

        public string LoginByToken(string token) {
            AccountJWTPayload payload = JWTUtils.Decode<AccountJWTPayload>(token);
            List<dynamic> rows = accountSQL.GetDataById(payload.Id);

            if (!rows.Any()) {
                return string.Empty;
            }

            return GetJWT(rows);
        }

        private string GetJWT(List<dynamic> rows) {
            return JWTUtils.Encode(new AccountJWTPayload {
                Id = rows[0].id,
                AccountKey = rows[0].account_key,
                AccountName = rows[0].account_name,
                Roles = rows.Select<dynamic, int>(row => row.role_id).ToList()
            });
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
            return GetData(rows);
        }

        public AccountPageDTO GetByPage(AccountPagination pagination) {
            return new AccountPageDTO() {
                Data = GetPageData(pagination),
                Total = accountSQL.GetCount(pagination)
            };
        }

        private List<AccountDTO> GetPageData(AccountPagination pagination) {
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
            return GetData(rows);
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

        public int Update(AccountDTO account) {
            Account data = new Account() {
                Id = account.Id,
                AccountKey = account.AccountKey,
                AccountName = account.AccountName
            };

            int res = accountSQL.Update(data) ? 1 : 0;

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
