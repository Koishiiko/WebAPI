using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.filter {
	public class LogFilter : IActionFilter {

		private ILogger<LogFilter> logger { get; }

		public LogFilter(ILogger<LogFilter> logger) {
			this.logger = logger;
		}

		public void OnActionExecuted(ActionExecutedContext context) {
			logger.LogInformation("GoodBye");
			logger.LogWarning("Warning");
			logger.LogDebug("Debug");
			logger.LogCritical("Critical");
			logger.LogTrace("Trace");
		}

		public void OnActionExecuting(ActionExecutingContext context) {
			logger.LogInformation("Hello");
		}
	}
}
