using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.po;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class ItemSQL : IItemSQL {

        public List<Item> getByModuleId(string id) {
            return DataSource.DB.Queryable<Item>().Where(i => i.ModuleId == id).OrderBy("item_id").ToList();
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
		                ON i.module_id = r.module_id AND i.item_id = r.item_id
	                LEFT JOIN suggestion s
		                ON i.module_id = s.module_id AND i.item_id = s.item_id
                WHERE i.module_id = @id
                ORDER BY i.item_id
			";
            return DataSource.QueryMany<ItemDataPO>(sql, new { id });
        }

        public Item getById(int id) {
            return DataSource.DB.Queryable<Item>().InSingle(id);
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
		                ON i.module_id = r.module_id AND i.item_id = r.item_id
	                LEFT JOIN suggestion s
		                ON i.module_id = s.module_id AND i.item_id = s.item_id
                WHERE i.module_id = @moduleId AND i.item_id = @itemId
                ORDER BY i.item_id
			";
            return DataSource.QueryMany<ItemDataPO>(sql, new { moduleId, itemId });
        }

        public long Save(Item item) {
            return DataSource.Save(item);
        }

        public bool Update(Item item) {
            return DataSource.Update(item);
        }

        public int Delete(string id) {
            return DataSource.DB.Deleteable<Item>().In(id).ExecuteCommand();
        }

        public int DeleteByModuleId(string id) {
            return DataSource.DB.Deleteable<Item>().Where(i => i.ModuleId == id).ExecuteCommand();
        }

        public int DeleteByStepId(int id) {
            string sql = @"
				DELETE FROM item WHERE EXISTS (SELECT step_id FROM module WHERE step_id = @id)
			";
            return DataSource.Delete(sql, new { id = id });
        }

        public int DeleteByItemId(string moduleId, string itemId) {
            return DataSource.DB.Deleteable<Item>().Where(i => i.ModuleId == moduleId).Where(i => i.ItemId == itemId).ExecuteCommand();
        }
    }
}
