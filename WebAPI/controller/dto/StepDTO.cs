using System.Collections.Generic;
using WebAPI.entity;

namespace WebAPI.dto {
	public class StepDTO {

		public int Id { get; set; }
		public int StepId { get; set; }
		public string Name { get; set; }
		public List<Module> Modules { get; set; }
	}
}
