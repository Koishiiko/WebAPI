using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace WebAPI.utils {
    /// <summary>
    /// 全局配置System.Text.Json不生效 导致解析出现问题
    /// 所以使用工具类的形式包装 统一配置
    /// </summary>
    public static class JSONUtils {

        private static readonly JsonSerializerOptions options = new() {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };

        public static string Serialize(object obj) {
            return JsonSerializer.Serialize(obj, options);
        }

        public static T Deserialize<T>(string json) {
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static object Deserialize(string json, Type type) {
            return JsonSerializer.Deserialize(json, type, options);
        }
    }
}
