using WebAPI.utils;

namespace WebAPI.exception {

    /// <summary>
    /// 发生500错误(或其他未捕获异常时)抛出的异常
    /// </summary>
    public class ExecuteError : CustomException {

        public ExecuteError() : base(ResultCode.SERVER_EXECUTED_ERROR) { }
    }
}
