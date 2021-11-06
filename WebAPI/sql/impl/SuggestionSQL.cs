using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class SuggestionSQL : ISuggestionSQL {

		private readonly IDataSource dataSource;

		public SuggestionSQL(IDataSource dataSource) {
			this.dataSource = dataSource;
		}

		public List<Suggestion> getByItemId(string moduleId, string itemId) {
			string sql = @"
				SELECT id, module_id, item_id, value FROM suggestion
				WHERE module_id = @moduleId AND item_id = @itemId
			";
			return dataSource.QueryMany<Suggestion>(sql, new { moduleId = moduleId, itemId = itemId });
		}

		public Suggestion getById(int id) {
			string sql = @"
				SELECT id, module_id, item_id, value FROM suggestion
				WHERE id = @id
			";
			return dataSource.QueryOne<Suggestion>(sql, new { id = id });
		}

		public long Save(Suggestion suggestion) {
			return dataSource.Save(suggestion);
		}

		public bool Update(Suggestion suggestion) {
			return dataSource.Update(suggestion);
		}

		public int Delete(int id) {
			string sql = @"
				DELETE FROM suggestion WHERE id = @id
			";
			return dataSource.Delete(sql, new { id = id });
		}

		public int DeleteByModuleId(string id) {
			string sql = @"
				DELETE FROM suggestion WHERE module_id = @id
			";
			return dataSource.Delete(sql, new { id = id });
		}

		public int DeleteByStepId(int id) {
			string sql = @"
				DELETE FROM suggestion WHERE step_id = @id
			";
			return dataSource.Delete(sql, new { id = id });
		}

		public int DeleteByItemId(string moduleId, string itemId) {
			string sql = @"
				DELETE FROM suggestion WHERE module_id = @moduleId AND item_id = @item_id
			";
			return dataSource.Delete(sql, new { moduleId, itemId });
		}
	}
}
