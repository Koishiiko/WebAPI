using System.Collections.Generic;
using SqlSugar;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class RuleSQL : IRuleSQL {

        public List<ItemRule> getAll() {
            return DataSource.Switch.Queryable<ItemRule>().ToList();
        }

        public ItemRule getById(int id) {
            return DataSource.Switch.Queryable<ItemRule>().InSingle(id);
        }

        public List<ItemRule> getByReportId(string reportId) {
            return DataSource.Switch.Queryable<ItemRule>()
                .Where(r => r.ReportId == reportId)
                .ToList();
        }

        public int Save(ItemRule rule) {
            return DataSource.Save(rule);
        }

        public int Update(ItemRule rule) {
            return DataSource.Update(rule);
        }

        public int Delete(int id) {
            return DataSource.Switch.Deleteable<ItemRule>().In(id).ExecuteCommand();
        }

        public int DeleteByModuleId(string moduleId) {
            return DataSource.Switch.Deleteable<ItemRule>().Where(ir => SqlFunc.Subqueryable<Module>().Where(m => m.ModuleId == moduleId).Any()).ExecuteCommand();
        }

        public int DeleteByStepId(int id) {
            return DataSource.Switch.Deleteable<ItemRule>().Where(ir => SqlFunc.Subqueryable<Module>().Where(m => m.StepId == id).Any()).ExecuteCommand();
        }


        public int DeleteByReportId(string reportId) {
            return DataSource.Switch.Deleteable<ItemRule>().Where(ir => ir.ReportId == reportId).ExecuteCommand();
        }
    }
}
