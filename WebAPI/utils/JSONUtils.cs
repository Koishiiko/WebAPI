using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.IO;
using System.Text.Unicode;

namespace WebAPI.utils {
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
