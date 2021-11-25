using System;
using System.Collections.Generic;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using JWT.Exceptions;
using WebAPI.exception;
using Newtonsoft.Json.Serialization;

namespace WebAPI.utils {
    public static class JWTUtils {

        private static readonly string SECRET = "THISISSECRET";

        private static readonly long EXPIRED_TIME = DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds();

        private static readonly IJwtAlgorithm algorithm = new HMACSHA256Algorithm();

        // 序列化和反序列化时使用驼峰命名
        private static readonly IJsonSerializer serializer = new JsonNetSerializer(new() {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        public static string Encode(AccountJWTPayload payload) {
            return GetJwtBuilder()
                   .AddClaim("exp", EXPIRED_TIME)
                   .AddClaim("data", payload)
                   .Encode();
        }

        public static T Decode<T>(string token) {
            try {
                // data为Newtonsoft.Json中的JObject对象
                return GetJwtBuilder()
                       .MustVerifySignature()
                       .Decode<IDictionary<string, dynamic>>(token)["data"].ToObject<T>(); ;
            } catch (TokenExpiredException) {
                throw new JWTException(ResultCode.TOKEN_IS_EXPIRED);
            } catch (InvalidTokenPartsException) {
                throw new JWTException(ResultCode.TOKEN_IS_INVALID);
            } catch (SignatureVerificationException) {
                throw new JWTException(ResultCode.TOKEN_VERIFY_ERROR);
            }
        }

        private static JwtBuilder GetJwtBuilder() {
            return JwtBuilder.Create().WithAlgorithm(algorithm).WithSecret(SECRET).WithSerializer(serializer);
        }
    }

    public class AccountJWTPayload {
        public int Id { get; set; }
        public string AccountKey { get; set; }
        public string AccountName { get; set; }
        public List<int> Roles { get; set; }
    }

}
