using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WebAPI.exception;
using WebAPI.utils;

namespace WebAPI.middleware {
    /// <summary>
    /// 全局异常捕获
    /// 
    /// 404 405等错误不会进入Filter中
    /// 所以这些错误的处理需要在该方法中实现
    /// </summary>
    public class GlobalExceptionMiddleware {

        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionMiddleware> log;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> log) {
            this.next = next;
            this.log = log;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await next(context);
                CheckStatusCode(context.Response.StatusCode);
            } catch (Exception e) {
                ExceptionResponseHandler(context, e);
            }
        }

        private void CheckStatusCode(int code) {
            if (code < 400) {
                return;
            }

            throw code switch {
                400 => new BadRequestException(),
                404 => new APINotFoundException(),
                405 => new MethodNotAllowException(),
                _ => new ExecuteError()
            };
        }

        private async void ExceptionResponseHandler(HttpContext context, Exception e) {
            log.LogError(
                $"[{context.Connection.RemoteIpAddress.MapToIPv4()}:{context.Connection.RemotePort}] {context.Request.Method} {context.Response.StatusCode}:" +
                    $" {context.Request.Path}{context.Request.QueryString}\n{e.GetType().FullName}: {e.Message}\n{e.StackTrace}\n");

            Result result = Result.Failure(e is CustomException ce ? ce.resultCode : ResultCode.SERVER_EXECUTED_ERROR);

            if (!context.Response.HasStarted) {
                await context.Response.WriteAsJsonAsync(result);
            }
        }

    }
}
