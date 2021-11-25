using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.utils;

namespace WebAPI.exception {
    public class BadRequestException : CustomException {

        public BadRequestException() : base(ResultCode.BAD_REQUEST) { }
    }
}
