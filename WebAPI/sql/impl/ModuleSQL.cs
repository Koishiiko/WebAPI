using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class ModuleSQL : IModuleSQL {

		private IDataSource dataSource { get; }

		public ModuleSQL(IDataSource dataSource) {
			this.dataSource = dataSource;
		}

		public Module getById(string id) {
			string sql = @"SELECT id, step_id, module_id, name FROM module WHERE module_id = @id";
			return dataSource.QueryOne<Module>(sql, new { id = id });
		}

		public List<Module> getByStepId(int id) {
			string sql = @"SELECT id, step_id, module_id, name FROM module WHERE step_id = @id";
			return dataSource.QueryMany<Module>(sql, new { id = id });
		}

		public int GetCountByStepId(int id) {
			string sql = @"SELECT COUNT(id) AS rows FROM module WHERE step_id = @id";
			return dataSource.QueryOne<int>(sql, new { id = id });
		}

		public long Save(Module module) {
			return dataSource.Save(module);
		}

		public bool Update(Module module) {
			return dataSource.Update(module);
		}

		public int Delete(int id) {
			string sql = @"DELETE FROM Module WHERE module_id = @id";
			return dataSource.Delete(sql, new { id = id });
		}

		public int DeleteByStepId(int id) {
			string sql = @"DELETE FROM Module WHERE step_id = @id";
			return dataSource.Delete(sql, new { id = id });
		}
	}
}
