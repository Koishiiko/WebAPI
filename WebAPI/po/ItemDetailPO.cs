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
    }
}
