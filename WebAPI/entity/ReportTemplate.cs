using SqlSugar;

namespace WebAPI.entity {
    [SugarTable("report_template")]
	public class ReportTemplate {

		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Path { get; set; }
	}
}
