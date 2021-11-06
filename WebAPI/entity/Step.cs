using Dapper.Contrib.Extensions;
using DapperExtensions.Mapper;

namespace WebAPI.entity {
	[Table("step")]
	public class Step {
		[Key]
		public int Id { get; set; }
		public int StepId { get; set; }
		public string Name { get; set; }
	}
	public sealed class StepCustomMapper : ClassMapper<Step> {
		public StepCustomMapper() {
			Table("step");
			Map(o => o.StepId).Column("step_id");
			AutoMap();
		}
	}
}
