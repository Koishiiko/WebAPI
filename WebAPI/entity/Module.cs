using SqlSugar;

namespace WebAPI.po {
	public class Module {
		public Module() {
		}

		public Module(int id, int stepId, string moduleId, string name) {
			Id = id;
			StepId = stepId;
			ModuleId = moduleId;
			Name = name;
		}

		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public int StepId { get; set; }
		public string ModuleId { get; set; }
		public string Name { get; set; }
	}
}
