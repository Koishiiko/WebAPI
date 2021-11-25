using WebAPI.utils;

namespace WebAPI.exception {
    /// <summary>
    /// 发生404错误时抛出的异常
    /// </summary>
    public class APINotFoundException : CustomException {

        public APINotFoundException() : base(ResultCode.API_NOT_FOUND) { } 
    }
}
