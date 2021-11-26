using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace WebAPI.po {
    public class StepData {
		public int SId { get; set; }
		public int StepId { get; set; }
		public string StepName { get; set; }
		public int MId { get; set; }
		public string ModuleId { get; set; }
		public string ModuleName { get; set; }
    }
}
