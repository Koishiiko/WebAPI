using System;

namespace WebAPI.attribute {
    /// <summary>
    /// 在控制器上添加该特性 或在接口方法上添加该特性
    /// 如果某个方法或它所在的类拥有该特性
    /// 就会对访问用户进行验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class AuthorizeAttribute : Attribute {
    }
}
