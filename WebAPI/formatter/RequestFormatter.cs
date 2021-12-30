using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using WebAPI.utils;

namespace WebAPI.formatter {
    [Obsolete("InputFormatter只能拦截化包含请求体的请求")]
    public class RequestFormatter : InputFormatter {

        public RequestFormatter() {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
        }

        protected override bool CanReadType(Type type) {
            return true;
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context) {
            var httpContext = context.HttpContext;
            var log = httpContext.RequestServices.GetRequiredService<ILogger<ResultFormatter>>();

            string bodyString;
            using (var reader = new StreamReader(httpContext.Request.Body)) {
                bodyString = await reader.ReadToEndAsync();
            }

            log.LogInformation($"[{httpContext.Connection.RemoteIpAddress.MapToIPv4()}:{httpContext.Connection.RemotePort}] {httpContext.Request.Method}:" +
                $" {httpContext.Request.Path}{httpContext.Request.QueryString} {bodyString}");

            return await InputFormatterResult.SuccessAsync(JSONUtils.Deserialize(bodyString));
        }
    }
}
