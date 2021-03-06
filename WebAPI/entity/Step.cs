using SqlSugar;

namespace WebAPI.entity {
	[SugarTable("step")]
	public class Step {

		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public int StepId { get; set; }
		public string Name { get; set; }
	}
}
