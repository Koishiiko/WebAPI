using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.attribute;
using System.Linq;
using WebAPI.utils;

namespace WebAPI.filter {
    public class ResultFilter : IActionFilter {

        /// <summary>
        /// Controller接口方行完成后进入的方法
        /// 将执行结果统一格式封装后
        /// 以JSON格式返回
        /// 
        /// 拥有UnpackageResultAttribute的接口所返回的结果不会被封装
        /// 出现异常时的返回结果由全局异常处理方法封装
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context) {
            if (IsUnpackageResult(context) || context.Exception != null) {
                return;
            }

            JsonResult jsonResult;
            dynamic result = context.Result;
            if (result is ObjectResult) {
                jsonResult = new JsonResult(result.Value is Result ? result.Value : Result.Success(result.Value));
            } else if (result is ContentResult) {
                jsonResult = new JsonResult(Result.Success(result.Content));
            } else {
                jsonResult = new JsonResult(Result.Success());
            }

            context.Result = jsonResult;
        }

        public void OnActionExecuting(ActionExecutingContext context) {
            // unimplemented
        }

        private bool IsUnpackageResult(in ActionExecutedContext context) {
            return context.ActionDescriptor.EndpointMetadata.Any(a => a.GetType() == typeof(UnpackageResultAttribute));
        }
    }
}
