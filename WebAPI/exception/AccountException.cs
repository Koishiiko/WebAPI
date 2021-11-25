using WebAPI.utils;

namespace WebAPI.exception {

    /// <summary>
    /// 发生500错误(或其他未捕获异常时)抛出的异常
    /// </summary>
    public class AccountException : CustomException {

        public AccountException(ResultCode resultCode) : base(resultCode) { }
    }
}
