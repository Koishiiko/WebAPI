using System.Collections.Generic;
using System.Linq;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.po;
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

        public List<Item> GetByModuleId(string moduleId) {
            return itemSQL.getByModuleId(moduleId);
        }

        public List<ItemDTO> GetDataByModuleId(string moduleId) {
            List<ItemDataPO> rows = itemSQL.getDataByModuleId(moduleId);
            List<ItemDTO> datas = new List<ItemDTO>();

            ItemDTO curr = null;
            rows.ForEach((row) => {
                if (curr == null || curr.Id != row.Id) {
                    ItemRule rules = row.RuleId != 0 ? new ItemRule {
                        Id = row.RuleId,
                        ModuleId = row.ModuleId,
                        ItemId = row.ItemId,
                        DefaultValue = row.DefaultValue,
                        Required = row.Required,
                        RequiredText = row.RequiredText,
                        MinValue = row.MinValue,
                        MinValueText = row.MinValueText,
                        MaxValue = row.MaxValue,
                        MaxValueText = row.MaxValueText,
                        MinLength = row.MinLength,
                        MinLengthText = row.MinLengthText,
                        MaxLength = row.MaxLength,
                        MaxLengthText = row.MaxLengthText
                    } : null;

                    curr = new ItemDTO {
                        Id = row.Id,
                        ModuleId = row.ModuleId,
                        ItemId = row.ItemId,
                        Name = row.Name,
                        Type = row.Type,
                        RecordId = row.RecordId,
                        ReportId = row.ReportId,
                        Rules = rules,
                        Suggestions = new List<Suggestion>()
                    };
                }
                if (row.SuggestionId != 0) {
                    Suggestion suggestion =
                        new Suggestion { Id = row.SuggestionId, ModuleId = row.ModuleId, ItemId = row.ItemId, Value = row.SuggestionValue };
                    curr.Suggestions.Add(suggestion);
                }
                if (!datas.Any() || datas.Last().Id != curr.Id) {
                    datas.Add(curr);
                }
            });
            return datas;
        }

        public ItemDTO getByItemId(string moduleId, string itemId) {
            List<ItemDataPO> rows = itemSQL.getByItemId(moduleId, itemId);
            if (!rows.Any()) {
                return null;
            }

            ItemRule rules = rows[0].RuleId != 0 ?
                    new ItemRule {
                        Id = rows[0].RuleId,
                        ModuleId = rows[0].ModuleId,
                        ItemId = rows[0].ItemId,
                        DefaultValue = rows[0].DefaultValue,
                        Required = rows[0].Required,
                        RequiredText = rows[0].RequiredText,
                        MinValue = rows[0].MinValue,
                        MinValueText = rows[0].MinValueText,
                        MaxValue = rows[0].MaxValue,
                        MaxValueText = rows[0].MaxValueText,
                        MinLength = rows[0].MinLength,
                        MinLengthText = rows[0].MinLengthText,
                        MaxLength = rows[0].MaxLength,
                        MaxLengthText = rows[0].MaxLengthText
                    } : null;

            ItemDTO data = new ItemDTO {
                Id = rows[0].Id,
                ModuleId = rows[0].ModuleId,
                ItemId = rows[0].ItemId,
                Name = rows[0].Name,
                Type = rows[0].Type,
                RecordId = rows[0].RecordId,
                ReportId = rows[0].ReportId,
                Rules = rules,
                Suggestions = new List<Suggestion>()
            };
            if (rows[0].SuggestionId != 0) {
                rows.ForEach((row) => {
                    data.Suggestions.Add(new Suggestion { Id = row.SuggestionId, ModuleId = row.ModuleId, ItemId = row.ItemId, Value = row.SuggestionValue });
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
