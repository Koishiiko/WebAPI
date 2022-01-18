using SqlSugar;

namespace WebAPI.entity {
    public class Item {

		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string ModuleId { get; set; }
		public string ItemId { get; set; }
		public string Name { get; set; }
		public int Type { get; set; }
		public string RecordId { get; set; }
		public string RecordName { get; set; }
		public string ReportId { get; set; }
	}
}
