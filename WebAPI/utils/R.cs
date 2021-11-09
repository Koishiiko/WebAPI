using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.utils {

	public class ResultCode {

		public int Code { get; }
		public string Message { get; }

		private ResultCode() { }

		private ResultCode(int code, string message) {
			Code = code;
			Message = message;
		}

		public static readonly ResultCode SUCCESS = new ResultCode(200, "执行成功");
		public static readonly ResultCode FAIL = new ResultCode(400, "执行失败");
		public static readonly ResultCode URL_NOT_FOUND = new ResultCode(404, "请求地址不存在");
		public static readonly ResultCode METHOD_NOT_ALLOWED = new ResultCode(405, "不支持当前请求方法");
		public static readonly ResultCode SERVER_EXECUTE_ERROR = new ResultCode(500, "服务端执行时出现错误");

		public static readonly ResultCode USER_NOT_LOGIN = new ResultCode(3001, "用户未登录");
		public static readonly ResultCode ACCOUNT_NOT_EXIST = new ResultCode(3002, "用户不存在");
		public static readonly ResultCode ACCOUNT_HAS_EXISTED = new ResultCode(3003, "用户已存在");
		public static readonly ResultCode PASSWORD_ERROR = new ResultCode(3004, "密码错误");
		public static readonly ResultCode ACCOUNT_OR_PASSWORD_ERROR = new ResultCode(3005, "用户或密码错误");
		public static readonly ResultCode LOGIN_ERROR = new ResultCode(3006, "登录时出现错误");
		public static readonly ResultCode PERMISSIONS_INSUFFICIENT = new ResultCode(3007, "访问权限不足");

		public static readonly ResultCode TOKEN_IS_INVALID = new ResultCode(6001, "登录令牌无效");
		public static readonly ResultCode TOKEN_IS_EXPIRED = new ResultCode(6001, "登录令牌已过期");
		public static readonly ResultCode TOKEN_VERIFY_ERROR = new ResultCode(6001, "令牌验证失败");
	}

	public class R {
		private ResultCode resultCode;

		public int Code => resultCode.Code;
		public string Message => resultCode.Message;
		public object Data { get; }

		private R() { }

		private R(ResultCode resultCode) {
			this.resultCode = resultCode;
			Data = null;
		}

		private R(ResultCode resultCode, object data) {
			this.resultCode = resultCode;
			Data = data;
		}

		public static R Success() {
			return new R(ResultCode.SUCCESS);
		}

		public static R Success(object data) {
			return new R(ResultCode.SUCCESS, data);
		}

		public static R Failure() {
			return new R(ResultCode.FAIL);
		}

		public static R Failure(ResultCode resultCode) {
			return new R(resultCode);
		}

		public static R Failure(ResultCode resultCode, object data) {
			return new R(resultCode, data);
		}
	}
}
