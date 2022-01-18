using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.pagination;
using WebAPI.po;

namespace WebAPI.sql {
	public interface IAccountSQL {

		List<Account> GetAll();

		Account GetById(int id);

		List<AccountDataPO> GetDataById(int id);

		Account GetByAccountKey(string accountKey);

		List<AccountDataPO> GetDataByAccountKey(string accountKey);

		List<AccountPagePO> GetByPage(AccountPagination pagination, out int total);

		int Save(Account account);

		int Update(Account account);

		int Delete(int id);
	}
}
