using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.po;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class ModuleSQL : IModuleSQL {

        public Module GetById(string id) {
            return DataSource.Switch.Queryable<Module>().Single(m => m.ModuleId == id);
        }

        public List<Module> GetByStepId(int id) {
            return DataSource.Switch.Queryable<Module>().Where(m => m.StepId == id).ToList();
        }

        public int GetCountByStepId(int id) {
            return DataSource.Switch.Queryable<Module>().Where(m => m.StepId == id).Count();
        }

        public int Save(Module module) {
            return DataSource.Save(module);
        }

        public int Update(Module module) {
            return DataSource.Update(module);
        }

        public int Delete(string id) {
            return DataSource.Switch.Deleteable<Module>().Where(m => m.ModuleId == id).ExecuteCommand();
        }

        public int DeleteByStepId(int id) {
            return DataSource.Switch.Deleteable<Module>().Where(m => m.StepId == id).ExecuteCommand();
        }
    }
}
