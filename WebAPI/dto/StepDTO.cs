using System.Collections.Generic;
using WebAPI.entity;

namespace WebAPI.dto {
	public class StepDTO {
		public StepDTO() {
		}

		public StepDTO(int id, int stepId, string name, List<Module> modules) {
			Id = id;
			StepId = stepId;
			Name = name;
			Modules = modules;
		}

		public int Id { get; set; }
		public int StepId { get; set; }
		public string Name { get; set; }
		public List<Module> Modules { get; set; }
	}
}
