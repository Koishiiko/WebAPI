using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.po;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class ItemSQL : IItemSQL {

        public List<Item> getByModuleId(string id) {
            return DataSource.Switch.Queryable<Item>().Where(i => i.ModuleId == id).OrderBy("item_id").ToList();
        }

        public List<ItemDataPO> getDataByModuleId(string id) {
            string sql = @"
                SELECT
                    i.id, i.module_id, i.item_id,
                    i.name, i.type, i.record_id, i.record_name, i.report_id,
                    r.id AS rule_id, r.default_value, r.required, r.required_text, 
                    r.min_value, r.min_value_text, r.max_value, r.max_value_text,
                    r.min_length, r.min_length_text, r.max_length, r.max_length_text,
                    s.id AS suggestion_id, s.value AS suggestion_value
                FROM item i
	                LEFT JOIN item_rule r
		                ON i.report_id = r.report_id
	                LEFT JOIN suggestion s
		                ON i.report_id = s.report_id 
                WHERE i.module_id = @id
                ORDER BY i.item_id
			";
            return DataSource.QueryMany<ItemDataPO>(sql, new { id });
        }

        public Item getById(int id) {
            return DataSource.Switch.Queryable<Item>().InSingle(id);
        }

        public List<ItemDataPO> getByItemId(string moduleId, string itemId) {
            string sql = @"
                SELECT
                    i.id, i.module_id, i.item_id,
                    i.name, i.type, i.record_id, i.record_name, i.report_id,
                    r.id AS rule_id, r.default_value, r.required, r.required_text,
                    r.min_value, r.min_value_text, r.max_value, r.max_value_text,
                    r.min_length, r.min_length_text, r.max_length, r.max_length_text,
                    s.id AS suggestion_id, s.value AS suggestion_value
                FROM item i
	                LEFT JOIN item_rule r
		                ON i.report_id = r.report_id 
	                LEFT JOIN suggestion s
		                ON i.report_id = s.report_id 
                WHERE i.module_id = @moduleId AND i.item_id = @itemId
                ORDER BY i.item_id
			";
            return DataSource.QueryMany<ItemDataPO>(sql, new { moduleId, itemId });
        }

        public List<ItemDataPO> getByReportId(string reportId) {
            string sql = @"
                SELECT
                    i.id, i.module_id, i.item_id,
                    i.name, i.type, i.record_id, i.record_name, i.report_id,
                    r.id AS rule_id, r.default_value, r.required, r.required_text,
                    r.min_value, r.min_value_text, r.max_value, r.max_value_text,
                    r.min_length, r.min_length_text, r.max_length, r.max_length_text,
                    s.id AS suggestion_id, s.value AS suggestion_value
                FROM item i
	                LEFT JOIN item_rule r
		                ON i.report_id = r.report_id
	                LEFT JOIN suggestion s
		                ON i.report_id = s.report_id
                WHERE i.report_id = @reportId
                ORDER BY i.item_id
			";
            return DataSource.QueryMany<ItemDataPO>(sql, new { reportId });
        }

        public List<ItemDetailPO> GetDataByStepId(int stepId) {
            return DataSource.Switch
                .Queryable<Module>()
                .LeftJoin<Item>((m, i) => m.ModuleId == i.ModuleId)
                .LeftJoin<ItemRule>((m, i, ir) => i.ReportId == ir.ReportId)
                .Where(m => m.StepId == stepId)
                .Select((m, i, ir) => new ItemDetailPO {
                    ModuleId = m.ModuleId,
                    ModuleName = m.Name,
                    ItemId = i.ItemId,
                    ItemName = i.Name,
                    Type = i.Type,
                    ReportId = i.ReportId,
                    Required = ir.Required,
                    MinValue = ir.MinValue,
                    MaxValue = ir.MaxValue,
                    MinLength = ir.MinLength,
                    MaxLength = ir.MaxLength
                })
                .ToList();

        }

        public int Save(Item item) {
            return DataSource.Save(item);
        }

        public int Update(Item item) {
            return DataSource.Update(item);
        }

        public int Delete(string id) {
            return DataSource.Switch.Deleteable<Item>().In(id).ExecuteCommand();
        }

        public int DeleteByModuleId(string id) {
            return DataSource.Switch.Deleteable<Item>().Where(i => i.ModuleId == id).ExecuteCommand();
        }

        public int DeleteByStepId(int id) {
            string sql = @"
				DELETE FROM item WHERE EXISTS (SELECT step_id FROM module WHERE step_id = @id)
			";
            return DataSource.Delete(sql, new { id = id });
        }

        public int DeleteByReportId(string reportId) {
            return DataSource.Switch.Deleteable<Item>().Where(i => i.ReportId == reportId).ExecuteCommand();
        }
    }
}
