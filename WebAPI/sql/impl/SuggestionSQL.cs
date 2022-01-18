using System.Collections.Generic;
using SqlSugar;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class SuggestionSQL : ISuggestionSQL {

        public List<Suggestion> getByItemId(string moduleId, string itemId) {
            return DataSource.Switch.Queryable<Suggestion>().Where(s => s.ModuleId == moduleId).Where(s => s.ItemId == itemId).ToList();
        }

        public Suggestion getById(int id) {
            return DataSource.Switch.Queryable<Suggestion>().InSingle(id);
        }

        public int Save(Suggestion suggestion) {
            return DataSource.Save(suggestion);
        }

        public int Update(Suggestion suggestion) {
            return DataSource.Update(suggestion);
        }

        public int Delete(int id) {
            return DataSource.Switch.Deleteable<Suggestion>().In(id).ExecuteCommand();
        }

        public int DeleteByModuleId(string id) {
            return DataSource.Switch.Deleteable<Suggestion>().Where(s => s.ModuleId == id).ExecuteCommand();
        }

        public int DeleteByStepId(int id) {
            return DataSource.Switch.Deleteable<Suggestion>().Where(ir => SqlFunc.Subqueryable<Module>().Where(m => m.StepId == id).Any()).ExecuteCommand();
        }

        public int DeleteByItemId(string moduleId, string itemId) {
            return DataSource.Switch.Deleteable<Suggestion>().Where(s => s.ModuleId == moduleId).Where(s => s.ItemId == itemId).ExecuteCommand();
        }
    }
}
