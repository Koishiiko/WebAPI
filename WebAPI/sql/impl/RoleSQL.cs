using System.Collections.Generic;
using WebAPI.pagination;
using WebAPI.entity;
using WebAPI.utils;
using WebAPI.po;

namespace WebAPI.sql.impl {
    public class RoleSQL : IRoleSQL {

        public int Delete(int id) {
            return DataSource.DB.Deleteable<Role>().In(id).ExecuteCommand();
        }

        public List<Role> GetAll() {
            return DataSource.DB.Queryable<Role>().ToList();
        }

        public List<Role> GetByIds(List<int> ids) {
            return DataSource.DB.Queryable<Role>().In(ids).ToList();
        }

        public List<Role> GetByPage(RolePagination pagination) {
            string sql = @"
                    SELECT id, name
                    FROM (
                        SELECT TOP (@end) id, name, ROW_NUMBER() OVER(ORDER BY id) AS n FROM role 
                    ) r
                    where r.n > @start
			";
            return DataSource.QueryMany<Role>(sql, new {
                start = pagination.Page * pagination.Size,
                end = (pagination.Page + 1) * pagination.Size
            });
        }

        public int GetCount(RolePagination pagination) {
            string sql = @"
				SELECT COUNT(id) AS rows FROM role
			";
            return DataSource.QueryOne<int>(sql);
        }

        public List<RoleDataPO> GetDataById(int id) {
            return DataSource.DB.Queryable<Role>()
                .LeftJoin<AccountRole>((r, ar) => r.Id == ar.RoleId)
                .Where(r => r.Id == id)
                .Select((r, ar) => new RoleDataPO { Id = r.Id, Name = r.Name, AccountId = ar.AccountId })
                .ToList();
        }

        public List<int> GetStepIdsByRoleId(int id) {
            return DataSource.DB.Queryable<RoleStep>().Where(rs => rs.RoleId == id)
                .Select(rs => rs.StepId)
                .ToList();
        }

        public long Save(Role role) {
            return DataSource.Save(role);
        }

        public bool Update(Role role) {
            return DataSource.Update(role);
        }
    }
}
