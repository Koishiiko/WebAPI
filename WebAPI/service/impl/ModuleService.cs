using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.sql;

namespace WebAPI.service.impl {
    public class ModuleService : IModuleService {

        private readonly IModuleSQL moduleSQL;
        private readonly IItemSQL itemSQL;
        private readonly IRuleSQL ruleSQL;
        private readonly ISuggestionSQL suggestionSQL;

        public ModuleService(IModuleSQL moduleSQL, IItemSQL itemSQL, IRuleSQL ruleSQL, ISuggestionSQL suggestionSQL){
            this.moduleSQL = moduleSQL;
            this.itemSQL = itemSQL;
            this.ruleSQL = ruleSQL;
            this.suggestionSQL = suggestionSQL;
        }

        public Module GetById(string id) {
            return moduleSQL.GetById(id);
        }

        public List<Module> GetByStepId(int id) {
            return moduleSQL.GetByStepId(id);
        }

        public long Save(Module module) {
            return moduleSQL.Save(module);
        }

        public int Update(Module module) {
            return moduleSQL.Update(module);
        }

        public int Delete(string id) {
            int res = moduleSQL.Delete(id);
            itemSQL.DeleteByModuleId(id);
            ruleSQL.DeleteByModuleId(id);
            suggestionSQL.DeleteByModuleId(id);
            return res;
        }
    }
}
