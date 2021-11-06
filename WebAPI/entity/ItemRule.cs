using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.entity {
	public class ItemRule {
		public ItemRule() {
		}

		public ItemRule(int id, string moduleId, string itemId, bool? required, string requiredText, float? minValue, string minValueText, float? maxValue, string maxValueText, int? minLength, string minLengthText, int? maxLength, string maxLengthText) {
			Id = id;
			Module_id = moduleId;
			Item_id = itemId;
			Required = required;
			Required_text = requiredText;
			Min_value = minValue;
			Min_value_text = minValueText;
			Max_value = maxValue;
			Max_value_text = maxValueText;
			Min_length = minLength;
			Min_length_text = minLengthText;
			Max_length = maxLength;
			Max_length_text = maxLengthText;
		}

		public int Id { get; set; }
		public string Module_id { get; set; }
		public string Item_id { get; set; }
		public bool? Required { get; set; }
		public string Required_text { get; set; }
		public float? Min_value { get; set; }
		public string Min_value_text { get; set; }
		public float? Max_value { get; set; }
		public string Max_value_text { get; set; }
		public int? Min_length { get; set; }
		public string Min_length_text { get; set; }
		public int? Max_length { get; set; }
		public string Max_length_text { get; set; }
	}
}
