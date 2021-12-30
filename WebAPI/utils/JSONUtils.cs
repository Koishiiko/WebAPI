using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebAPI.utils {
    public static class JSONUtils {

        private static readonly JsonSerializerSettings settings = new() {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static string Serialize(object obj) {
            return JsonConvert.SerializeObject(obj, settings);
        }

        public static T Deserialize<T>(string json) {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static object Deserialize(string json) {
            return JsonConvert.DeserializeObject(json, settings);
        }
    }
}
