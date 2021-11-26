using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.po;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class ItemSQL : IItemSQL {

		public List<Item> getByModuleId(string id) {
			string sql = @"
				SELECT
					id, module_id, item_id, name, type, record_id, report_id
				FROM item WHERE module_id = @id
                ORDER BY item_id
			";
			return DataSource.QueryMany<Item>(sql, new { id = id });
		}

		public List<dynamic> getDataByModuleId(string id) {
			string sql = @"
                SELECT
                    i.id, i.module_id, i.item_id,
                    i.name, i.type, i.record_id, i.report_id,
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
			return DataSource.QueryMany<dynamic>(sql, new { id = id });
		}

		public Item getById(int id) {
			string sql = @"
				SELECT
					id, module_id, item_id, name, type, record_id, report_id
				FROM item WHERE id = @id
                ORDER BY item_id
			";
			return DataSource.QueryOne<Item>(sql, new { id = id });
		}

		public List<dynamic> getByItemId(string moduleId, string itemId) {
			string sql = @"
                SELECT
                    i.id, i.module_id, i.item_id,
                    i.name, i.type, i.record_id, i.report_id,
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
			return DataSource.QueryMany<dynamic>(sql, new { moduleId, itemId });
		}

		public long Save(Item item) {
			return DataSource.Save(item);
		}

		public bool Update(Item item) {
			return DataSource.Update(item);
		}

		public int Delete(string id) {
			string sql = @"
				DELETE FROM item WHERE id = @id
			";
			return DataSource.Delete(sql, new { id = id });
		}

		public int DeleteByModuleId(string id) {
			string sql = @"
				DELETE FROM item WHERE module_id = @id
			";
			return DataSource.Delete(sql, new { id = id });
		}

		public int DeleteByStepId(int id) {
			string sql = @"
				DELETE FROM item WHERE EXISTS (SELECT step_id FROM module WHERE step_id = @id)
			";
			return DataSource.Delete(sql, new { id = id });
		}

		public int DeleteByItemId(string moduleId, string itemId) {
			string sql = @"
				DELETE FROM item WHERE module_id = @moduleId AND item_id = @itemId
			";
			return DataSource.Delete(sql, new { moduleId, itemId });
		}
	}
}
