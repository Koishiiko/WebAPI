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

		public AccountService(IAccountSQL accountSQL) {
			this.accountSQL = accountSQL;
		}

		public List<Account> GetAll() {
			return accountSQL.GetAll();
		}

		public Account GetByAccountKey(string accountKey) {
			return accountSQL.GetByAccountKey(accountKey);
		}

		public AccountPageDTO GetByPage(AccountPagination pagination) {
			return new AccountPageDTO() {
				Data = this.GetData(pagination),
				Total = accountSQL.GetCount(pagination)
			}
		}

		private List<AccountDTO> GetData(AccountPagination pagination) {
			var res = accountSQL.GetByPage(pagination);
			return null;
		}

		public AccountDTO GetDataById(int id) {
			throw new NotImplementedException();
		}

		public string Login(Account account) {
			throw new NotImplementedException();
		}

		public string LoginByToken(string token) {
			throw new NotImplementedException();
		}

		public long Save(AccountDTO account) {
			throw new NotImplementedException();
		}

		public bool Updata(AccountDTO account) {
			throw new NotImplementedException();
		}

		public int Delete(int id) {
			throw new NotImplementedException();
		}
	}
}
