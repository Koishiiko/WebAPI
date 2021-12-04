using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using WebAPI.utils;

namespace WebAPI.filter {
    /// <summary>
    /// 日志打印过滤器
    /// 
    /// 分别打印请求日志和响应日志
    /// </summary>
    public class LogFilter : IActionFilter {

        private readonly ILogger<LogFilter> log;

        public LogFilter(ILogger<LogFilter> log) {
            this.log = log;
        }

        public void OnActionExecuting(ActionExecutingContext context) {
            var httpContext = context.HttpContext;

            log.LogInformation(
                $"[{httpContext.Connection.RemoteIpAddress.MapToIPv4()}:{httpContext.Connection.RemotePort}] {httpContext.Request.Method}:" +
                    $" {httpContext.Request.Path}{httpContext.Request.QueryString} {(context.ActionArguments.Count > 0 ? JSONUtils.Serialize(context.ActionArguments) : "")}");
        }

        public void OnActionExecuted(ActionExecutedContext context) {
            var httpContext = context.HttpContext;
            
            log.LogInformation(
                $"[{httpContext.Connection.RemoteIpAddress.MapToIPv4()}:{httpContext.Connection.RemotePort}] {httpContext.Request.Method} {httpContext.Response.StatusCode}:" +
                    $" {httpContext.Request.Path}{httpContext.Request.QueryString}");
        }
    }
}
