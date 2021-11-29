using WebAPI.utils;

namespace WebAPI.exception {
    /// <summary>
    /// 400错误时抛出的异常
    /// </summary>
    public class BadRequestException : CustomException {

        public BadRequestException() : base(ResultCode.BAD_REQUEST) { }
    }
}
