using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.sql;
using WebAPI.po;
using WebAPI.dto;
using WebAPI.pagination;

namespace WebAPI.service {
	public interface IAccountService {

		string Login(Account account);

		string LoginByToken(string token);

		List<Account> GetAll();

		AccountPageDTO GetByPage(AccountPagination pagination);

		Account GetById(int id);

		AccountDTO GetDataById(int id);

		Account GetByAccountKey(string accountKey);

		AccountDTO GetDataByAccountKey(string accountKey);

		long Save(AccountDTO account);

		int Update(AccountDTO account);

		int Delete(int id);
	}
}
