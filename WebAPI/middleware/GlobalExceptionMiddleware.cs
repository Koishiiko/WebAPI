using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebAPI.utils;
using System.Text.Json;
using WebAPI.exception;
using Microsoft.Extensions.Logging;

namespace WebAPI.middleware {
    public class GlobalExceptionMiddleware {

        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionMiddleware> log;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> log) {
            this.next = next;
            this.log = log;
        }

        /// <summary>
        /// 404 405等错误不会进入Filter中
        /// 所以这些错误的处理需要在该方法中实现
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context) {
            try {
                await next(context);
                CheckStatusCode(context.Response.StatusCode);
            } catch (Exception e) {
                ExceptionResponseHandler(context, e);
            }
        }

        private void CheckStatusCode(int code) {
            if (code == 200) {
                return;
            }

            throw code switch {
                400 => new BadRequestException(),
                404 => new APINotFoundException(),
                405 => new MethodNotAllowException(),
                _ => new ExecuteError(),
            };
        }

        private async void ExceptionResponseHandler(HttpContext context, Exception e) {
            log.LogError($"[{context.Connection.RemoteIpAddress}] {context.Request.Method} {context.Response.StatusCode}: {context.Request.Path}{context.Request.QueryString} {e.Message}\n{e.StackTrace}\n");

            Result result = Result.Failure(e is CustomException ? (e as CustomException).resultCode : ResultCode.SERVER_EXECUTE_ERROR);
            // 在Startup.cs中只能配置Controller范围内的JsonSerializer
            // 暂时不知道如何全局配置 
            await JsonSerializer.SerializeAsync(context.Response.Body, result,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

    }
}
