using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.po {
    public class ItemDataPO {

        public int Id { get; set; }
        public string ModuleId { get; set; }
        public string ItemId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string RecordId { get; set; }
        public string RecordName { get; set; }
        public string ReportId { get; set; }
        public int RuleId { get; set; }
        public string DefaultValue { get; set; }
		public bool? Required { get; set; }
		public string RequiredText { get; set; }
		public double? MinValue { get; set; }
		public string MinValueText { get; set; }
		public double? MaxValue { get; set; }
		public string MaxValueText { get; set; }
		public int? MinLength { get; set; }
		public string MinLengthText { get; set; }
		public int? MaxLength { get; set; }
		public string MaxLengthText { get; set; }
        public int SuggestionId { get; set; }
        public string SuggestionValue { get; set; }

    }
}
