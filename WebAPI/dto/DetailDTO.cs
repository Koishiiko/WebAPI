using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;

namespace WebAPI.dto {
	public class DetailDTO {

		public int Id { get; set; }
		public string TestGuid { get; set; }
		public string ModuleKey { get; set; }
		public string ModuleName { get; set; }
		public string ItemKey { get; set; }
		public string ItemName { get; set; }
		public string RecordKey { get; set; }
		public string RecordName { get; set; }
		public string RecordValue { get; set; }
	}
}
