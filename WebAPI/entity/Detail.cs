using SqlSugar;

namespace WebAPI.entity {
    [SugarTable("test_detail")]
	public class Detail {

		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string TestGuid { get; set; }
		public string ModuleKey { get; set; }
		public string ItemKey { get; set; }
		public string RecordKey { get; set; }
		public string RecordValue { get; set; }
	}
}
