using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;
using WebAPI.pagination;
using WebAPI.po;

namespace WebAPI.sql {
	public interface IRoleSQL {

		List<Role> GetAll();

		List<Role> GetByIds(List<int> ids);

		List<RoleDataPO> GetDataById(int id);

		List<int> GetStepIdsByRoleId(int id);

		List<Role> GetByPage(RolePagination pagination);

		int GetCount(RolePagination pagination);

		long Save(Role role);

		bool Update(Role role);

		int Delete(int id);
	}
}
