using SqlSugar;

namespace WebAPI.entity {
    public class Role {

		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
