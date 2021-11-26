using System.Collections.Generic;
using System.Linq;
using WebAPI.dto;
using WebAPI.po;
using WebAPI.sql;

namespace WebAPI.service.impl {
    public class ItemService : IItemService {

        private IItemSQL itemSQL { get; }
        private IRuleSQL ruleSQL { get; }
        private ISuggestionSQL suggestionSQL { get; }

        public ItemService(IItemSQL itemSQL, IRuleSQL ruleSQL, ISuggestionSQL suggestionSQL) {
            this.itemSQL = itemSQL;
            this.ruleSQL = ruleSQL;
            this.suggestionSQL = suggestionSQL;
        }

        public List<Item> GetByModuleId(string moduleId) {
            return itemSQL.getByModuleId(moduleId);
        }

        public List<ItemDTO> GetDataByModuleId(string moduleId) {
            List<dynamic> rows = itemSQL.getDataByModuleId(moduleId);
            List<ItemDTO> datas = new List<ItemDTO>();

            ItemDTO curr = null;
            rows.ForEach((row) => {
                if (curr == null || curr.Id != row.id) {
                    ItemRule rules = row.rule_id != null ? new ItemRule {
                        Id = row.rule_id,
                        ModuleId = row.module_id,
                        ItemId = row.item_id,
                        DefaultValue = row.default_value,
                        Required = row.required,
                        RequiredText = row.required_text,
                        MinValue = row.min_value,
                        MinValueText = row.min_value_text,
                        MaxValue = row.max_value,
                        MaxValueText = row.max_value_text,
                        MinLength = row.min_length,
                        MinLengthText = row.min_length_text,
                        MaxLength = row.max_length,
                        MaxLengthText = row.max_length_text
                    } : null;

                    curr = new ItemDTO {
                        Id = row.id,
                        ModuleId = row.module_id,
                        ItemId = row.item_id,
                        Name = row.name,
                        Type = row.type,
                        RecordId = row.record_id,
                        ReportId = row.report_id,
                        Rules = rules,
                        Suggestions = new List<Suggestion>()
                    };
                }
                if (row.suggestion_id != null) {
                    Suggestion suggestion =
                        new Suggestion { Id = row.suggestion_id, ModuleId = row.module_id, ItemId = row.item_id, Value = row.suggestion_value };
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

            ItemRule rules = rows[0].rule_id != null ?
                    new ItemRule {
                        Id = rows[0].rule_id,
                        ModuleId = rows[0].module_id,
                        ItemId = rows[0].item_id,
                        DefaultValue = rows[0].default_value,
                        Required = rows[0].required,
                        RequiredText = rows[0].required_text,
                        MinValue = rows[0].min_value,
                        MinValueText = rows[0].min_value_text,
                        MaxValue = rows[0].max_value,
                        MaxValueText = rows[0].max_value_text,
                        MinLength = rows[0].min_length,
                        MinLengthText = rows[0].min_length_text,
                        MaxLength = rows[0].max_length,
                        MaxLengthText = rows[0].max_length_text
                    } : null;

            ItemDTO data = new ItemDTO {
                Id = rows[0].id,
                ModuleId = rows[0].module_id,
                ItemId = rows[0].item_id,
                Name = rows[0].name,
                Type = rows[0].type,
                RecordId = rows[0].record_id,
                ReportId = rows[0].report_id,
                Rules = rules,
                Suggestions = new List<Suggestion>()
            };
            if (rows[0].suggestion_id != null) {
                rows.ForEach((row) => {
                    data.Suggestions.Add(new Suggestion { Id = row.suggestion_id, ModuleId = row.module_id, ItemId = row.item_id, Value = row.suggestion_value });
                });
            }
            return data;
        }

        public long Save(ItemDTO item) {
            Item data =
                new Item { Id = item.Id, ModuleId = item.ModuleId, ItemId = item.ItemId, Name = item.Name, Type = item.Type, RecordId = item.RecordId, ReportId = item.ReportId };
            long id = itemSQL.Save(data);

            ItemRule rules = item.Rules != null ? item.Rules : new ItemRule();
            rules.ModuleId = item.ModuleId;
            rules.ItemId = item.ItemId;
            ruleSQL.Save(rules);

            if (item.Suggestions != null && item.Suggestions.Any()) {
                item.Suggestions.ForEach((suggestion) => {
                    suggestion.ModuleId = item.ModuleId;
                    suggestion.ItemId = item.ItemId;
                    suggestionSQL.Save(suggestion);
                });
            }
            return id;
        }

        public int Update(ItemDTO item) {
            Item data =
                new Item { Id = item.Id, ModuleId = item.ModuleId, ItemId = item.ItemId, Name = item.Name, Type = item.Type, RecordId = item.RecordId, ReportId = item.ReportId };
            int res = itemSQL.Update(data) ? 1 : 0;

            ItemRule rules = item.Rules != null ? item.Rules : new ItemRule();
            rules.ModuleId = item.ModuleId;
            rules.ItemId = item.ItemId;
            if (rules.Id == 0) {
                ruleSQL.Save(rules);
            } else {
                ruleSQL.Update(rules);
            }

            suggestionSQL.DeleteByItemId(item.ModuleId, item.ItemId);
            if (item.Suggestions != null && item.Suggestions.Any()) {
                item.Suggestions.ForEach((suggestion) => {
                    suggestion.ModuleId = item.ModuleId;
                    suggestion.ItemId = item.ItemId;
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
