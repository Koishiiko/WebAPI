﻿using SqlSugar;

namespace WebAPI.entity {
    public class Suggestion {

		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string ModuleId { get; set; }
		public string ItemId { get; set; }
		public string Value { get; set; }
	}
}
