using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.pagination;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class RoleSQL : IRoleSQL {

		private IDataSource dataSource { get; }

		public RoleSQL(IDataSource dataSource) {
			this.dataSource = dataSource;
		}

		public int Delete(int id) {
			string sql = @"
				DELETE FROM role WHERE id = @id
			";
			return dataSource.Delete(sql, new { id });
		}

		public int DeleteStepsByRoleId(int id) {
			string sql = @"
                DELETE FROM role_step WHERE role_id = @id
			";
			return dataSource.Delete(sql, new { id });
		}

		public List<Role> GetAll() {
			string sql = @"
                SELECT id, name FROM role
			";
			return dataSource.QueryMany<Role>(sql);
		}

		public List<Role> GetByPage(RolePagination pagination) {
			string sql = @"
                    SELECT id, name
                    FROM (
                        SELECT TOP (@end) id, name, ROW_NUMBER() OVER(ORDER BY id) AS n FROM role 
                    ) r
                    where r.n > @start
			";
			return dataSource.QueryMany<Role>(sql, new {
				start = pagination.Page * pagination.Size,
				end = (pagination.Page + 1) * pagination.Size
			});
		}

		public int GetCount(RolePagination pagination) {
			string sql = @"
				SELECT COUNT(id) AS rows FROM role
			";
			return dataSource.QueryOne<int>(sql);
		}

		public List<dynamic> GetDataById(int id) {
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
			return dataSource.QueryMany<dynamic>(sql, new { id });
		}

		public List<int> GetStepIdsByRoleId(int id) {
			string sql = @"
				SELECT step_id FROM role_step WHERE role_id = @id
			";
			return dataSource.QueryMany<int>(sql, new { id });
		}

		public long Save(Role role) {
			return dataSource.Save(role);
		}

		public bool Update(Role role) {
			return dataSource.Update(role);
		}
	}
}
