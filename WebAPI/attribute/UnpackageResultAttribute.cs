using System;

namespace WebAPI.attribute {

    /// <summary>
    /// 拥有该特性的Controller接口方法在返回时
    /// 不会经过封装filter/ResultFilter的封装
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class UnpackageResultAttribute : Attribute {
    }
}
