using Dapper.Contrib.Extensions;

namespace WebAPI.entity {
	[Table("valid")]
	public class Valid {
		[Key]
		public int Id { get; set; }
		public string Guid { get; set; }
		public string Module_id { get; set; }
		public string Value { get; set; }
	}
}
