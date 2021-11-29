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
            string sql = @"
               SELECT
                   r.id, r.name, ar.account_id
               FROM
                   role r
                   LEFT JOIN
                       account_role ar
                   ON r.id = ar.role_id
               WHERE
                   r.id = @id
			";
            return DataSource.QueryMany<RoleDataPO>(sql, new { id });
        }

        public List<int> GetStepIdsByRoleId(int id) {
            string sql = @"
				SELECT step_id FROM role_step WHERE role_id = @id
			";
            return DataSource.QueryMany<int>(sql, new { id });
        }

        public long Save(Role role) {
            return DataSource.Save(role);
        }

        public bool Update(Role role) {
            return DataSource.Update(role);
        }
    }
}
