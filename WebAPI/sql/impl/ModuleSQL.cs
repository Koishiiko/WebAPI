using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class ModuleSQL : IModuleSQL {

        public Module GetById(string id) {
            return DataSource.DB.Queryable<Module>().Single(m => m.ModuleId == id);
        }

        public List<Module> GetByStepId(int id) {
            return DataSource.DB.Queryable<Module>().Where(m => m.StepId == id).ToList();
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
            return DataSource.DB.Deleteable<Module>().Where(m => m.ModuleId == id).ExecuteCommand();
        }

        public int DeleteByStepId(int id) {
            return DataSource.DB.Deleteable<Module>().Where(m => m.StepId == id).ExecuteCommand();
        }
    }
}
