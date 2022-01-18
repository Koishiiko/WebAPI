using System.Collections.Generic;
using System.Linq;
using WebAPI.dto;
using WebAPI.pagination;
using WebAPI.entity;
using WebAPI.sql;
using WebAPI.po;

namespace WebAPI.service.impl {
    public class RoleService : IRoleService {

        private readonly IRoleSQL roleSQL;
        private readonly IAccountRoleSQL accountRoleSQL;
        private readonly IRoleStepSQL roleStepSQL;


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
                Data = roleSQL.GetByPage(pagination, out int total),
                Total = total
            };
        }

        public RoleDTO GetDataById(int id) {
            List<RoleDataPO> rows = roleSQL.GetDataById(id);
            if (!rows.Any()) {
                return null;
            }

            RoleDTO data = new RoleDTO() {
                Id = rows[0].Id,
                Name = rows[0].Name,
                StepIds = roleSQL.GetStepIdsByRoleId(id)
            };

            if (rows[0].AccountId != 0) {
                data.AccountIds = new List<int>();
                rows.ForEach((row) => {
                    data.AccountIds.Add(row.AccountId);
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

        public int Update(RoleDTO role) {
            Role data = new Role() { Id = role.Id, Name = role.Name };
            int res = roleSQL.Update(data);

            if (res == 1) {
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
