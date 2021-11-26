using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.po;
using WebAPI.pagination;

namespace WebAPI.sql {
	public interface IAccountSQL {

		List<Account> GetAll();

		Account GetById(int id);

		List<AccountDataPO> GetDataById(int id);

		Account GetByAccountKey(string accountKey);

		List<AccountDataPO> GetDataByAccountKey(string accountKey);

		List<AccountPagePO> GetByPage(AccountPagination pagination);

		int GetCount(AccountPagination pagination);

		long Save(Account account);

		bool Update(Account account);

		int Delete(int id);
	}
}
