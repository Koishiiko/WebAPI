using SqlSugar;

namespace WebAPI.po {
	public class Valid {
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string Guid { get; set; }
		public string ModuleId { get; set; }
		public bool Value { get; set; }
	}
}
