using System.Security.Cryptography;
using System.Text;

namespace WebAPI.utils {
    public static class MD5Utils {

        private static readonly MD5 md5 = MD5.Create();

        private static readonly string SECRET = "XMGYSECRET";

        public static readonly string DEFAULT_PASSWORD = GetMD5("123456");

        public static string GetMD5(string str) {
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str + SECRET));

            var sb = new StringBuilder();
            foreach (var b in bytes) {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
