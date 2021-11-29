using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class SuggestionSQL : ISuggestionSQL {

        public List<Suggestion> getByItemId(string moduleId, string itemId) {
            return DataSource.DB.Queryable<Suggestion>().Where(s => s.ModuleId == moduleId).Where(s => s.ItemId == itemId).ToList();
        }

        public Suggestion getById(int id) {
            return DataSource.DB.Queryable<Suggestion>().InSingle(id);
        }

        public long Save(Suggestion suggestion) {
            return DataSource.Save(suggestion);
        }

        public bool Update(Suggestion suggestion) {
            return DataSource.Update(suggestion);
        }

        public int Delete(int id) {
            return DataSource.DB.Deleteable<Suggestion>().In(id).ExecuteCommand();
        }

        public int DeleteByModuleId(string id) {
            return DataSource.DB.Deleteable<Suggestion>().Where(s => s.ModuleId == id).ExecuteCommand();
        }

        public int DeleteByStepId(int id) {
            string sql = @"
				DELETE FROM suggestion WHERE EXISTS (SELECT module_id FROM module WHERE step_id = @id)
			";
            return DataSource.Delete(sql, new { id = id });
        }

        public int DeleteByItemId(string moduleId, string itemId) {
            return DataSource.DB.Deleteable<Suggestion>().Where(s => s.ModuleId == moduleId).Where(s => s.ItemId == itemId).ExecuteCommand();
        }
    }
}
