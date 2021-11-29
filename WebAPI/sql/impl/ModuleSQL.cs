using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class ModuleSQL : IModuleSQL {

        public Module GetById(string id) {
            string sql = @"SELECT id, step_id, module_id, name FROM module WHERE module_id = @id";
            return DataSource.QueryOne<Module>(sql, new { id = id });
        }

        public List<Module> GetByStepId(int id) {
            string sql = @"SELECT id, step_id, module_id, name FROM module WHERE step_id = @id";
            return DataSource.QueryMany<Module>(sql, new { id = id });
        }

        public int GetCountByStepId(int id) {
            string sql = @"SELECT COUNT(id) AS rows FROM module WHERE step_id = @id";
            return DataSource.QueryOne<int>(sql, new { id = id });
        }

        public long Save(Module module) {
            return DataSource.Save(module);
        }

        public bool Update(Module module) {
            return DataSource.Update(module);
        }

        public int Delete(string id) {
            string sql = @"DELETE FROM Module WHERE module_id = @id";
            return DataSource.Delete(sql, new { id = id });
        }

        public int DeleteByStepId(int id) {
            string sql = @"DELETE FROM Module WHERE step_id = @id";
            return DataSource.Delete(sql, new { id = id });
        }
    }
}
