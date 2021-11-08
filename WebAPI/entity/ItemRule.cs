using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace WebAPI.entity {
	[SugarTable("item_rule")]
	public class ItemRule {
		public ItemRule() {
		}

		public ItemRule(int id, string moduleId, string itemId, bool? required, string requiredText, float? minValue, string minValueText, float? maxValue, string maxValueText, int? minLength, string minLengthText, int? maxLength, string maxLengthText) {
			Id = id;
			ModuleId = moduleId;
			ItemId = itemId;
			Required = required;
			RequireText = requiredText;
			MinValue = minValue;
			MinValueText = minValueText;
			MaxValue = maxValue;
			MaxValueText = maxValueText;
			MinLength = minLength;
			MinLengthText = minLengthText;
			MaxLength = maxLength;
			MaxLengthText = maxLengthText;
		}

		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string ModuleId { get; set; }
		public string ItemId { get; set; }
		public bool? Required { get; set; }
		public string RequireText { get; set; }
		public float? MinValue { get; set; }
		public string MinValueText { get; set; }
		public float? MaxValue { get; set; }
		public string MaxValueText { get; set; }
		public int? MinLength { get; set; }
		public string MinLengthText { get; set; }
		public int? MaxLength { get; set; }
		public string MaxLengthText { get; set; }
	}
}
