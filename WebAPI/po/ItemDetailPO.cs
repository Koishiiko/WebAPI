using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.po {
    public class ItemDetailPO {

        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public int Type { get; set; }
        public string ReportId { get; set; }
        public bool? Required { get; set; }
        public double? MinValue { get; set; } 
        public double? MaxValue { get; set; }
        public int? MinLength { get; set; } 
        public int? MaxLength { get; set; }

    }
}
