using System;
using WebAPI.utils;

namespace WebAPI.exception {
    public class CustomException : Exception {

        public ResultCode resultCode { get; }

        public override string Message => resultCode.Message;

        public CustomException(ResultCode resultCode) {
            this.resultCode = resultCode;
        }
    }
}
