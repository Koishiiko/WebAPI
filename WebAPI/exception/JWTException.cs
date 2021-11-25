﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.utils;

namespace WebAPI.exception {

    public class JWTException : CustomException {

        public JWTException(ResultCode resultCode) : base(resultCode) {
        }
    }
}