using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace WebAPI.entity {
	[Table("report_template")]
	public class ReportTemplate {
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Path { get; set; }
	}
}
