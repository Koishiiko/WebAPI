using WebAPI.utils;

namespace WebAPI.exception {

    /// <summary>
    /// 发生405错误时抛出的异常
    /// </summary>
    public class MethodNotAllowException : CustomException {

        public MethodNotAllowException() : base(ResultCode.METHOD_NOT_ALLOWED) { }
    }
}
