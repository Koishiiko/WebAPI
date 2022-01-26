using SqlSugar;

namespace WebAPI.entity {
    public class Suggestion {

		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string ReportId { get; set; }
		public string Value { get; set; }
	}
}
