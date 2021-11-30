using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

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

            // XXX: 优化逻辑(暂时没有找到其他判断是否上传文件的方式)
            // 访问Requesst.Form.Files时 如果没有上传文件
            // 就会抛出InvalidOperationException异常
            string bodyString;
            try {
                _ = httpContext.Request.Form.Files;
                bodyString = $"content size: {httpContext.Request.ContentLength} bytes";
            } catch (InvalidOperationException) {
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
        }
    }
}
