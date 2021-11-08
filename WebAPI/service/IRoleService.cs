using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;
using WebAPI.sql;
using WebAPI.pagination;

namespace WebAPI.service {
	public interface IRoleService {

		List<Role> GetAll();

		RoleDTO GetDataById(int id);

		RolePageDTO GetByPage(RolePagination pagination);

		long Save(RoleDTO role);

		bool Update(RoleDTO role);

		int Delete(int id);
	}
}
