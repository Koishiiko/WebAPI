using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace WebAPI.entity {
	public class Record {
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string RecordId { get; set; }
		public string Name { get; set; }
	}
}
