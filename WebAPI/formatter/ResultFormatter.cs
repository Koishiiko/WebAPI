using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using WebAPI.utils;

namespace WebAPI.formatter {
    public class ResultFormatter : OutputFormatter {

        public ResultFormatter() {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
        }

        protected override bool CanWriteType(Type type) {
            return true;
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context) {
            var httpContext = context.HttpContext;

            var log = httpContext.RequestServices.GetRequiredService<ILogger<ResultFormatter>>();

            Result result;
            if (context.Object is Result) {
                result = context.Object as Result;
            } else {
                result = Result.Success(context.Object);
            }

            httpContext.Response.ContentType = "applictaion/json";
            string bodyString = JSONUtils.Serialize(result);
            httpContext.Response.WriteAsync(bodyString);

            log.LogInformation(
                $"[{httpContext.Connection.RemoteIpAddress}] {httpContext.Request.Method}: {httpContext.Request.Path}{httpContext.Request.QueryString} {bodyString}");

            return Task.CompletedTask;
        }
    }
}
