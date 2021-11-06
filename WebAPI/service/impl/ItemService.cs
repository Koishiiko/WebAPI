using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.sql;

namespace WebAPI.service.impl {
	public class ItemService : IItemService {

		private readonly IItemSQL itemSQL;
		private readonly IRuleSQL ruleSQL;
		private readonly ISuggestionSQL suggestionSQL;

		public ItemService(IItemSQL itemSQL, IRuleSQL ruleSQL, ISuggestionSQL suggestionSQL) {
			this.itemSQL = itemSQL;
			this.ruleSQL = ruleSQL;
			this.suggestionSQL = suggestionSQL;
		}

		public List<Item> getByModuleId(string moduleId) {
			return itemSQL.getByModuleId(moduleId);
		}

		public List<ItemDTO> getDataByModuleId(string moduleId) {
			List<dynamic> rows = itemSQL.getDataByModuleId(moduleId);
			List<ItemDTO> datas = new List<ItemDTO>();

			ItemDTO curr = null;
			rows.ForEach((row) => {
				if (curr == null || curr.Id != row.id) {
					ItemRule rules = row.rule_id != null ? new ItemRule(row.rule_id, row.module_id, row.item_id,
									row.required, row.required_text,
									row.minValue, row.minValueText, row.maxValue, row.maxValueText,
									row.minLength, row.minLengthText, row.maxLength, row.maxLengthText)
									: null;

					curr = new ItemDTO(
								row.id, row.module_id, row.item_id, row.name, row.type,
								row.record_id, row.report_id,
								rules,
								new List<Suggestion>()
					);
				}
				if (row.suggestion_id != null) {
					Suggestion suggestion =
						new Suggestion(row.suggestion_id, row.module_id, row.item_id, row.suggestion_value);
					curr.Suggestions.Add(suggestion);
				}
				if (!datas.Any() || datas.Last().Id != curr.Id) {
					datas.Add(curr);
				}
			});
			return datas;
		}

		public ItemDTO getByItemId(string moduleId, string itemId) {
			List<dynamic> rows = itemSQL.getByItemId(moduleId, itemId);
			if (!rows.Any()) {
				return null;
			}

			ItemRule rules = rows[0].rule_id != null ? new ItemRule(rows[0].rule_id, rows[0].module_id, rows[0].item_id,
								rows[0].required, rows[0].required_text,
								rows[0].minValue, rows[0].minValueText, rows[0].maxValue, rows[0].maxValueText,
								rows[0].minLength, rows[0].minLengthText, rows[0].maxLength, rows[0].maxLengthText)
								: null;

			ItemDTO data = new ItemDTO(
							rows[0].id, rows[0].module_id, rows[0].item_id, rows[0].name, rows[0].type,
							rows[0].record_id, rows[0].report_id,
							rules,
							new List<Suggestion>()
			);
			rows.ForEach((row) => {
				data.Suggestions.Add(new Suggestion(row.suggestion_id, row.module_id, row.item_id, row.suggestion_value));
			});
			return data;
		}

		public long Save(ItemDTO item) {
			Item data =
				new Item(item.Id, item.ModuleId, item.ItemId, item.Name, item.Type, item.RecordId, item.ReportId);
			long id = itemSQL.Save(data);

			ItemRule rules = item.Rules;
			rules.Module_id = item.ModuleId;
			rules.Item_id = item.ItemId;
			ruleSQL.Save(rules);

			if (item.Suggestions.Any()) {
				item.Suggestions.ForEach((suggestion) => {
					suggestion.Module_id = item.ModuleId;
					suggestion.Item_id = item.ItemId;
					suggestionSQL.Save(suggestion);
				});
			}
			return id;
		}

		public bool Update(ItemDTO item) {
			Item data =
				new Item(item.Id, item.ModuleId, item.ItemId, item.Name, item.Type, item.RecordId, item.ReportId);
			bool res = itemSQL.Update(data);

			ItemRule rules = item.Rules;
			rules.Module_id = item.ModuleId;
			rules.Item_id = item.ItemId;
			ruleSQL.Update(item.Rules);

			suggestionSQL.DeleteByItemId(item.ModuleId, item.ItemId);
			if (item.Suggestions.Any()) {
				item.Suggestions.ForEach((suggestion) => {
					suggestion.Module_id = item.ModuleId;
					suggestion.Item_id = item.ItemId;
					suggestionSQL.Save(suggestion);
				});
			}
			return res;
		}

		public int Delete(string moduleId, string itemId) {
			int count = itemSQL.DeleteByItemId(moduleId, itemId);
			ruleSQL.DeleteByItemId(moduleId, itemId);
			suggestionSQL.DeleteByItemId(moduleId, itemId);
			return count;
		}
	}
}
