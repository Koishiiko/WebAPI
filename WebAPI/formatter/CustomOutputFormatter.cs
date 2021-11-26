using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebAPI.utils;
using System.Text.Json;

namespace WebAPI.formatter {
    public class CustomOutputFormatter : TextOutputFormatter {

        public CustomOutputFormatter() {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type) {
            return true;
        }

        public async override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding) {
            var httpContext = context.HttpContext;

            string responseString;
            if (context.Object is Result) {
                responseString = JsonSerializer.Serialize(context.Object, typeof(Result));
            } else {
            }

            //await httpContext.Response.WriteAsync("Hello", selectedEncoding);
        }
    }
}
