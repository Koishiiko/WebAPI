using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.sql;
using WebAPI.entity;
using WebAPI.dto;
using WebAPI.pagination;

namespace WebAPI.service {
	public interface IAccountService {

		string Login(Account account);

		string LoginByToken(string token);

		List<Account> GetAll();

		AccountPageDTO GetByPage(AccountPagination pagination);

		AccountDTO GetDataById(int id);

		Account GetByAccountKey(string accountKey);

		long Save(AccountDTO account);

		bool Updata(AccountDTO account);

		int Delete(int id);
	}
}
