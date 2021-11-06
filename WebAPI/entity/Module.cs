namespace WebAPI.entity {
	public class Module {
		public Module() {
		}

		public Module(int id, int stepId, string moduleId, string name) {
			Id = id;
			Step_id = stepId;
			Module_id = moduleId;
			Name = name;
		}

		public int Id { get; set; }
		public int Step_id { get; set; }
		public string Module_id { get; set; }
		public string Name { get; set; }
	}
}
