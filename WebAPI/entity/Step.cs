using SqlSugar;

namespace WebAPI.entity {
	[SugarTable("step")]
	public class Step {
		[SugarColumn(ColumnName = "id", IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		[SugarColumn(ColumnName = "step_id")]
		public int StepId { get; set; }
		[SugarColumn(ColumnName = "name")]
		public string Name { get; set; }
	}
}
