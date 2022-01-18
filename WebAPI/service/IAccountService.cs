using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.sql;
using WebAPI.entity;
using WebAPI.dto;
using WebAPI.pagination;
using WebAPI.utils;

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

		int ChangePassword(AccountPayload payload, PasswordDTO passwords);

		long Save(AccountDTO account);

		int Update(AccountDTO account);

		int Delete(int id);
	}
}
