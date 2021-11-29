using System.Collections.Generic;

namespace WebAPI.dto {
    public class MaterialDTO {
        // { 工序id: { 模块id: { 报告id: 记录值, ...}, ... }, ... }
        public IDictionary<int, IDictionary<string, IDictionary<string, string>>> Data { get; set; }
        // { 工序id: { 模块id: 是否填写完成, ...}, ... }
        public IDictionary<int, IDictionary<string, bool>> Valids { get; set; }
    }
}
