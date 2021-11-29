using WebAPI.utils;

namespace WebAPI.exception {

    /// <summary>
    /// 用户登录失败时抛出的异常
    /// </summary>
    public class AccountException : CustomException {

        public AccountException(ResultCode resultCode) : base(resultCode) { }
    }
}
