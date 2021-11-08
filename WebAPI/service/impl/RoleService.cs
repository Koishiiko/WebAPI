using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.pagination;
using WebAPI.sql;

namespace WebAPI.service.impl {
	public class RoleService : IRoleService {

		private IRoleSQL roleSQL { get; }
		private IAccountRoleSQL accountRoleSQL { get; }
		private IRoleStepSQL roleStepSQL { get; }


		public RoleService(IRoleSQL roleSQL, IAccountRoleSQL accountRoleSQL, IRoleStepSQL roleStepSQL) {
			this.roleSQL = roleSQL;
			this.accountRoleSQL = accountRoleSQL;
			this.roleStepSQL = roleStepSQL;
		}

		public int Delete(int id) {
			return roleSQL.Delete(id);
		}

		public List<Role> GetAll() {
			return roleSQL.GetAll();
		}

		public RolePageDTO GetByPage(RolePagination pagination) {
			return new RolePageDTO() {
				Data = roleSQL.GetByPage(pagination),
				Total = roleSQL.GetCount(pagination)
			};
		}

		public RoleDTO GetDataById(int id) {
			List<dynamic> rows = roleSQL.GetDataById(id);
			if (!rows.Any()) {
				return null;
			}

			RoleDTO data = new RoleDTO() {
				Id = rows[0].id,
				Name = rows[0].name,
				StepIds = roleSQL.GetStepIdsByRoleId(id)
			};

			if (rows[0].account_id != null) {
				data.AccountIds = new List<int>();
				rows.ForEach((row) => {
					data.AccountIds.Add(row.account_id);
				});
			}

			return data;
		}

		public long Save(RoleDTO role) {
			Role data = new Role() { Id = role.Id, Name = role.Name };
			long id = roleSQL.Save(data);

			if (role.StepIds.Any()) {
				roleStepSQL.SaveSteps(id, role.StepIds);
			}

			if (role.AccountIds.Any()) {
				accountRoleSQL.SaveAccounts(id, role.AccountIds);
			}

			return id;
		}

		public bool Update(RoleDTO role) {
			Role data = new Role() { Id = role.Id, Name = role.Name };
			bool res = roleSQL.Update(data);

			if (res) {
				roleStepSQL.DeleteByRoleId(role.Id);
				if (role.StepIds.Any()) {
					roleStepSQL.SaveSteps(role.Id, role.StepIds);
				}

				accountRoleSQL.DeleteByRoleId(role.Id);
				if (role.AccountIds.Any()) {
					accountRoleSQL.SaveAccounts(role.Id, role.AccountIds);
				}
			}

			return res;
		}
	}
}
