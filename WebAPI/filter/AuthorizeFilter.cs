using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.attribute;
using WebAPI.utils;

namespace WebAPI.filter {
    public class AuthorizeFilter : IAsyncActionFilter {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            if (HasAuthorize(context.ActionDescriptor.EndpointMetadata)) {
                string token = context.HttpContext.Request.Headers["Authorization"];
                var payload = JWTUtils.Decode<AccountPayload>(token);

                // TODO: 根据接口进行身份认证
                // 可以直接在AuthorizeAttribute中添加
                // 或者根据数据库中角色对应的权限进行判断
            }

            await next();
        }

        private bool HasAuthorize(in IList<object> attributes) {
            foreach (var attribute in attributes) {
                if (attribute is UnauthorizeAttribute) {
                    return false;
                }
                if (attribute is AuthorizeAttribute) {
                    return true;
                }
            }
            return false;
        }
    }

}
