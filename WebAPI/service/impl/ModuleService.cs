﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.po;
using WebAPI.sql;

namespace WebAPI.service.impl {
    public class ModuleService : IModuleService {

        private IModuleSQL moduleSQL { get; }
        private IItemSQL itemSQL { get; }
        private IRuleSQL ruleSQL { get; }
        private ISuggestionSQL suggestionSQL { get; }

        public ModuleService(IModuleSQL moduleSQL, IItemSQL itemSQL, IRuleSQL ruleSQL, ISuggestionSQL suggestionSQL){
            this.moduleSQL = moduleSQL;
            this.itemSQL = itemSQL;
            this.ruleSQL = ruleSQL;
            this.suggestionSQL = suggestionSQL;
        }

        public Module GetById(string id) {
            return moduleSQL.getById(id);
        }

        public List<Module> GetByStepId(int id) {
            return moduleSQL.getByStepId(id);
        }

        public long Save(Module module) {
            return moduleSQL.Save(module);
        }

        public int Update(Module module) {
            return moduleSQL.Update(module) ? 1 : 0;
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
