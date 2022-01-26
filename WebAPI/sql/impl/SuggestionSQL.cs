using System.Collections.Generic;
using SqlSugar;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class SuggestionSQL : ISuggestionSQL {

        public List<Suggestion> getByReportId(string reportId) {
            return DataSource.Switch.Queryable<Suggestion>().Where(s => s.ReportId == reportId).ToList();
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
            return DataSource.Switch.Deleteable<Suggestion>().Where(s => SqlFunc.Subqueryable<Module>().Where(m => m.ModuleId == id).Any()).ExecuteCommand();
        }

        public int DeleteByStepId(int id) {
            return DataSource.Switch.Deleteable<Suggestion>().Where(s => SqlFunc.Subqueryable<Module>().Where(m => m.StepId == id).Any()).ExecuteCommand();
        }

        public int DeleteByReportId(string reportId) {
            return DataSource.Switch.Deleteable<Suggestion>().Where(s => s.ReportId == reportId).ExecuteCommand();
        }
    }
}
