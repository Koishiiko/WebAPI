using System.Collections.Generic;
using System.Linq;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.pagination;
using WebAPI.sql;
using WebAPI.utils;
using WebAPI.exception;
using WebAPI.po;

namespace WebAPI.service.impl {
    public class AccountService : IAccountService {

        private readonly IAccountSQL accountSQL;
        private readonly IAccountRoleSQL accountRoleSQL;

        public AccountService(IAccountSQL accountSQL, IAccountRoleSQL accountRoleSQL) {
            this.accountSQL = accountSQL;
            this.accountRoleSQL = accountRoleSQL;
        }

        public string Login(Account account) {
            List<AccountDataPO> rows = accountSQL.GetDataByAccountKey(account.AccountKey);

            if (!rows.Any() || rows[0].Password != account.Password) {
                throw new AccountException(ResultCode.ACCOUNT_OR_PASSWORD_ERROR);
            }
            return GetJWT(rows);
        }

        public string LoginByToken(string token) {
            AccountPayload payload = JWTUtils.Decode<AccountPayload>(token);
            List<AccountDataPO> rows = accountSQL.GetDataById(payload.Id);

            if (!rows.Any()) {
                return string.Empty;
            }
            return GetJWT(rows);
        }

        private string GetJWT(in List<AccountDataPO> rows) {
            return JWTUtils.Encode(new AccountPayload {
                Id = rows[0].Id,
                AccountKey = rows[0].AccountKey,
                AccountName = rows[0].AccountName,
                Roles = rows.Select(row => row.RoleId).ToList()
            });
        }

        public List<Account> GetAll() {
            return accountSQL.GetAll();
        }

        public Account GetByAccountKey(string accountKey) {
            return accountSQL.GetByAccountKey(accountKey);
        }

        public AccountDTO GetDataByAccountKey(string accountKey) {
            List<AccountDataPO> rows = accountSQL.GetDataByAccountKey(accountKey);

            if (!rows.Any()) {
                return null;
            }
            return GetData(rows);
        }

        public AccountPageDTO GetByPage(AccountPagination pagination) {
            List<AccountPagePO> rows = accountSQL.GetByPage(pagination, out int total);
            List<AccountDTO> accounts = new List<AccountDTO>();

            AccountDTO curr = null;
            rows.ForEach((row) => {
                if (curr == null || curr.Id != row.Id) {
                    curr = new AccountDTO() {
                        Id = row.Id,
                        AccountKey = row.AccountKey,
                        AccountName = row.AccountName,
                        Roles = new List<Role>()
                    };
                }
                if (row.RoleId != 0) {
                    curr.Roles.Add(
                        new Role() {
                            Id = row.RoleId,
                            Name = row.RoleName
                        });
                }

                if (!accounts.Any() || accounts.Last().Id != curr.Id) {
                    accounts.Add(curr);
                }
            });

            return new AccountPageDTO() {
                Data = accounts,
                Total = total
            };
        }

        public Account GetById(int id) {
            return accountSQL.GetById(id);
        }

        public AccountDTO GetDataById(int id) {
            List<AccountDataPO> rows = accountSQL.GetDataById(id);

            if (!rows.Any()) {
                return null;
            }
            return GetData(rows);
        }

        private AccountDTO GetData(in List<AccountDataPO> rows) {
            AccountDTO data = new AccountDTO() {
                Id = rows[0].Id,
                AccountKey = rows[0].AccountKey,
                AccountName = rows[0].AccountName,
                Roles = new List<Role>()
            };

            if (rows[0].RoleId != 0) {
                rows.ForEach((row) => {
                    data.Roles.Add(new Role() {
                        Id = row.RoleId
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
