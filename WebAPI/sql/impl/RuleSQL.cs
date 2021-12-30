using System.Collections.Generic;
using SqlSugar;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class RuleSQL : IRuleSQL {

        public List<ItemRule> getAll() {
            return DataSource.DB.Queryable<ItemRule>().ToList();
        }

        public ItemRule getById(int id) {
            return DataSource.DB.Queryable<ItemRule>().InSingle(id);
        }

        public List<ItemRule> getByItemId(string moduleId, string itemId) {
            return DataSource.DB.Queryable<ItemRule>()
                .Where(r => r.ModuleId == moduleId)
                .Where(r => r.ItemId == itemId)
                .ToList();
        }

        public long Save(ItemRule rule) {
            return DataSource.Save(rule);
        }

        public bool Update(ItemRule rule) {
            return DataSource.Update(rule);
        }

        public int Delete(int id) {
            return DataSource.DB.Deleteable<ItemRule>().In(id).ExecuteCommand();
        }

        public int DeleteByModuleId(string id) {
            return DataSource.DB.Deleteable<ItemRule>().Where(ir => ir.ModuleId == id).ExecuteCommand();
        }

        public int DeleteByStepId(int id) {
            return DataSource.DB.Deleteable<ItemRule>().Where(ir => SqlFunc.Subqueryable<Module>().Where(m => m.StepId == id).Any()).ExecuteCommand();
        }

        public int DeleteByItemId(string moduleId, string itemId) {
            return DataSource.DB.Deleteable<ItemRule>().Where(ir => ir.ModuleId == moduleId).Where(ir => ir.ItemId == itemId).ExecuteCommand();
        }
    }
}
