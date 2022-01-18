using System.Collections.Generic;
using WebAPI.pagination;
using WebAPI.entity;
using WebAPI.utils;
using WebAPI.po;

namespace WebAPI.sql.impl {
    public class RoleSQL : IRoleSQL {

        public int Delete(int id) {
            return DataSource.Switch.Deleteable<Role>().In(id).ExecuteCommand();
        }

        public List<Role> GetAll() {
            return DataSource.Switch.Queryable<Role>().ToList();
        }

        public List<Role> GetByIds(List<int> ids) {
            return DataSource.Switch.Queryable<Role>().In(ids).ToList();
        }

        public List<Role> GetByPage(RolePagination pagination, out int total) {
            total = 0;
            return DataSource.Switch.Queryable<Role>().ToPageList(pagination.Page, pagination.Size, ref total);
        }

        public List<RoleDataPO> GetDataById(int id) {
            return DataSource.Switch.Queryable<Role>()
                .LeftJoin<AccountRole>((r, ar) => r.Id == ar.RoleId)
                .Where(r => r.Id == id)
                .Select((r, ar) => new RoleDataPO { Id = r.Id, Name = r.Name, AccountId = ar.AccountId })
                .ToList();
        }

        public List<int> GetStepIdsByRoleId(int id) {
            return DataSource.Switch.Queryable<RoleStep>().Where(rs => rs.RoleId == id)
                .Select(rs => rs.StepId)
                .ToList();
        }

        public int Save(Role role) {
            return DataSource.Save(role);
        }

        public int Update(Role role) {
            return DataSource.Update(role);
        }
    }
}
