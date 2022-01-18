using System.Collections.Generic;

namespace WebAPI.dto {

    public class MaterialDTO {

        public StepsData Data { get; set; }
        public StepsValid Valids { get; set; }
    }

    /**
     * 代替typedef功能
     * using生成的别名只在当前文件下有效
     */
    // key: reportId, value: recordValue
    public class ItemsData : Dictionary<string, string> { }
    // key: moduleId
    public class ModulesData : Dictionary<string, ItemsData> { }
    // key: stepId
    public class StepsData : Dictionary<int, ModulesData> { }
    // key: moduleId, value: moduleValid
    public class ModulesValid : Dictionary<string, bool> { }
    // key stepId
    public class StepsValid : Dictionary<int, ModulesValid> { }
}
