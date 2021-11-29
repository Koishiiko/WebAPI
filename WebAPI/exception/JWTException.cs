using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.utils;

namespace WebAPI.exception {

    /// <summary>
    /// JWT验证失败时抛出的异常
    /// </summary>
    public class JWTException : CustomException {

        public JWTException(ResultCode resultCode) : base(resultCode) {
        }
    }
}
