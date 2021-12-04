using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using WebAPI.utils;

namespace WebAPI.formatter {
    /// <summary>
    /// 响应格式器
    /// 
    /// 将返回的对象统一封装成Result对象(JSON格式)并返回
    /// </summary>
    public class ResultFormatter : OutputFormatter {

        public ResultFormatter() {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
        }

        protected override bool CanWriteType(Type type) {
            return true;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context) {
            var httpContext = context.HttpContext;

            Result result = context.Object is Result r ? r : Result.Success(context.Object);

            string bodyString = JSONUtils.Serialize(result);
            httpContext.Response.ContentType = "applictaion/json";
            await httpContext.Response.WriteAsync(bodyString);

            /// 如果需要打印返回结果的话 可以在这里打印日志
            //var log = httpContext.RequestServices.GetRequiredService<ILogger<ResultFormatter>>();
            //log.LogInformation(
            //    $"[{httpContext.Connection.RemoteIpAddress.MapToIPv4()}:{httpContext.Connection.RemotePort}] {httpContext.Request.Method}: {httpContext.Request.Path}{httpContext.Request.QueryString} {bodyString}");
        }
    }
}
