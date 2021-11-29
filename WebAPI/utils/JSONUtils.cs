using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace WebAPI.utils {
    public static class JSONUtils {

        private static readonly JsonSerializerOptions options = new() {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public static string Serialize(object obj) {
            return JsonSerializer.Serialize(obj, options);
        }

        public static T? Deserialize<T>(string json) {
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static object? Deserialize(string json, Type type) {
            return JsonSerializer.Deserialize(json, type, options);
        }
    }
}
