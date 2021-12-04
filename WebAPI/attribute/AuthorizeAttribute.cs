using System;

namespace WebAPI.attribute {
    /// <summary>
    /// 在控制器上添加该特性 或在接口方法上添加该特性
    /// 会使(控制器下的)方法在执行之前进行身份验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class AuthorizeAttribute : Attribute {
    }
}
