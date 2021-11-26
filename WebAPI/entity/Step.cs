using SqlSugar;

namespace WebAPI.po {
	[SugarTable("step")]
	public class Step {
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public int StepId { get; set; }
		public string Name { get; set; }
	}
}
