using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.dto {
	public class MaterialDTO {

		public IDictionary<int, IDictionary<string, IDictionary<string, string>>> Data { get; set; }
		public IDictionary<int, IDictionary<string, bool>> Valids { get; set; }
	}
}
