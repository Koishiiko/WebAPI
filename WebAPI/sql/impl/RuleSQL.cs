using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.po;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class RuleSQL : IRuleSQL {

		public List<ItemRule> getAll() {
			string sql = @"
				SELECT
					id, item_id,
					required, required_text,
					min_value, min_value_text, max_value, max_value_text,
					min_length, min_length_text, max_length, max_length_text
				FROM item_rule
			";
			return DataSource.QueryMany<ItemRule>(sql);
		}

		public ItemRule getById(int id) {
			string sql = @"
				SELECT
					id, item_id,
					required, required_text,
					min_value, min_value_text, max_value, max_value_text,
					min_length, min_length_text, max_length, max_length_text
				FROM item_rule
				WHERE id = @id
			";
			return DataSource.QueryOne<ItemRule>(sql, new { id = id });
		}

		public List<ItemRule> getByItemId(string moduleId, string itemId) {
			string sql = @"
				SELECT
					id, item_id,
					required, required_text,
					min_value, min_value_text, max_value, max_value_text,
					min_length, min_length_text, max_length, max_length_text
				FROM item_rule
				WHERE module_id = @moduleId AND item_id = @itemId
			";
			return DataSource.QueryMany<ItemRule>(sql, new { moduleId = moduleId, itemId = itemId });
		}

		public long Save(ItemRule rule) {
			return DataSource.Save(rule);
		}

		public bool Update(ItemRule rule) {
			return DataSource.Update(rule);
		}

		public int Delete(int id) {
			string sql = @"
				DELETE FROM item_rule WHERE id = @id
			";
			return DataSource.Delete(sql, new { id = id });
		}

		public int DeleteByModuleId(string id) {
			string sql = @"
				DELETE FROM item_rule WHERE module_id = @id
			";
			return DataSource.Delete(sql, new { id = id });
		}

		public int DeleteByStepId(int id) {
			string sql = @"
				DELETE FROM item_rule WHERE EXISTS (SELECT module_id FROM module WHERE step_id = @id)
			";
			return DataSource.Delete(sql, new { id = id });
		}

		public int DeleteByItemId(string moduleId, string itemId) {
			string sql = @"
				DELETE FROM item_rule WHERE module_id = @moduleId AND item_id = @itemId
			";
			return DataSource.Delete(sql, new { moduleId, itemId });
		}
	}
}
