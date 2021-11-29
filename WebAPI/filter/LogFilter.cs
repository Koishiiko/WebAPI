using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebAPI.utils;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http;

namespace WebAPI.filter {
    public class LogFilter : IActionFilter {

        private readonly ILogger<LogFilter> log;

        public LogFilter(ILogger<LogFilter> log) {
            this.log = log;
        }

        /// <summary>
        /// 前置过滤器
        /// 
        /// 在请求进入Controller方法前 打印日志
        /// </summary>
        /// <param name="context"></param>
        public async void OnActionExecuting(ActionExecutingContext context) {
            var httpContext = context.HttpContext;

            string bodyString;
            if (httpContext.Request.ContentLength > 1 * 1024) {
                bodyString = $"content size: {httpContext.Request.ContentLength}";
            } else {
                httpContext.Request.EnableBuffering();
                using (var reader = new StreamReader(httpContext.Request.Body)) {
                    bodyString = await reader.ReadToEndAsync();
                    httpContext.Request.Body.Position = 0;
                }
            }

            log.LogInformation($"[{httpContext.Connection.RemoteIpAddress}] {httpContext.Request.Method}:" +
                $" {httpContext.Request.Path}{httpContext.Request.QueryString} {bodyString}");
        }

        [System.Obsolete("响应日志在ResultFormatter中打印")]
        public void OnActionExecuted(ActionExecutedContext context) {
            //if (context.Exception != null) {
            //    return;
            //}

            //var httpContext = context.HttpContext;
            //string bodyString = context.Result is JsonResult ? JsonSerializer.Serialize((context.Result as JsonResult).Value, typeof(Result),
            //    new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }) : "";
            //log.LogInformation($"[{httpContext.Connection.RemoteIpAddress}] {httpContext.Request.Method}:" +
            //    $" {httpContext.Request.Path}{httpContext.Request.QueryString} {bodyString}");
        }
    }
}
