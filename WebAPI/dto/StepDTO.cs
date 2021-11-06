using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;

namespace WebAPI.dto {
	public class StepDTO {
		public StepDTO() {
		}

		public StepDTO(int id, int step_id, string name, List<Module> modules) {
			Id = id;
			Step_id = step_id;
			Name = name;
			Modules = modules;
		}

		public int Id { get; set; }
		public int Step_id { get; set; }
		public string Name { get; set; }
		public List<Module> Modules { get; set; }
	}
}
