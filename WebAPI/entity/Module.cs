using SqlSugar;

namespace WebAPI.entity {
    public class Module {

        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public int StepId { get; set; }
        public string ModuleId { get; set; }
        public string Name { get; set; }
    }
}
