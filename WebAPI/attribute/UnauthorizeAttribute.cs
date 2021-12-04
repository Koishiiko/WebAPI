using System;

namespace WebAPI.attribute {
    /// <summary>
    /// 添加了该特性的方法 不会经过身份验证
    /// 
    /// 用于设置控制器下特定接口不进行验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class UnauthorizeAttribute : Attribute {
    }
}
