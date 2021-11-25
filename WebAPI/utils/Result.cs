﻿namespace WebAPI.utils {

	public class ResultCode {

		public int Code { get; }
		public string Message { get; }

		private ResultCode() { }

		private ResultCode(int code, string message) {
			Code = code;
			Message = message;
		}

		public static readonly ResultCode SUCCESS = new(200, "执行成功");
		public static readonly ResultCode FAIL = new (4000, "执行失败");

		public static readonly ResultCode BAD_REQUEST = new (400, "请求参数有误");
		public static readonly ResultCode API_NOT_FOUND = new (404, "请求地址不存在");
		public static readonly ResultCode METHOD_NOT_ALLOWED = new (405, "不支持当前请求方法");
		public static readonly ResultCode SERVER_EXECUTE_ERROR = new (500, "服务端执行时出现错误");

		public static readonly ResultCode USER_NOT_LOGIN = new (3001, "用户未登录");
		public static readonly ResultCode ACCOUNT_NOT_EXIST = new (3002, "用户不存在");
		public static readonly ResultCode ACCOUNT_HAS_EXISTED = new (3003, "用户已存在");
		public static readonly ResultCode PASSWORD_ERROR = new (3004, "密码错误");
		public static readonly ResultCode ACCOUNT_OR_PASSWORD_ERROR = new (3005, "用户或密码错误");
		public static readonly ResultCode LOGIN_ERROR = new (3006, "登录时出现错误");
		public static readonly ResultCode PERMISSIONS_INSUFFICIENT = new (3007, "访问权限不足");

		public static readonly ResultCode TOKEN_IS_INVALID = new (6001, "登录令牌无效");
		public static readonly ResultCode TOKEN_IS_EXPIRED = new (6002, "登录令牌已过期");
		public static readonly ResultCode TOKEN_VERIFY_ERROR = new (6003, "令牌验证失败");
	}

	public class Result {

		private ResultCode resultCode;

		public int Code => resultCode.Code;
		public string Message => resultCode.Message;
		public object Data { get; }

		private Result() { }

		private Result(ResultCode resultCode) {
			this.resultCode = resultCode;
			Data = null;
		}

		private Result(ResultCode resultCode, object data) {
			this.resultCode = resultCode;
			Data = data;
		}

		public static Result Success() {
			return new Result(ResultCode.SUCCESS);
		}

		public static Result Success(object data) {
			return new Result(ResultCode.SUCCESS, data);
		}

		public static Result Failure() {
			return new Result(ResultCode.FAIL);
		}

		public static Result Failure(ResultCode resultCode) {
			return new Result(resultCode);
		}

		public static Result Failure(ResultCode resultCode, object data) {
			return new Result(resultCode, data);
		}
	}
}