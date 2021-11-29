using System.Collections.Generic;

namespace WebAPI.dto {
    public class RoleDTO {

		public int Id { get; set; }
		public string Name { get; set; }
		public List<int> AccountIds { get; set; }
		public List<int> StepIds { get; set; }
	}
}
