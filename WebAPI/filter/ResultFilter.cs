using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.filter {
	public class ResultFilter : IActionFilter {
		public void OnActionExecuted(ActionExecutedContext context) {
			Console.WriteLine("result:goodbye");
		}

		public void OnActionExecuting(ActionExecutingContext context) {
			Console.WriteLine("result:hello");
		}
	}
}
