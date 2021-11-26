using System.Collections.Generic;
using WebAPI.po;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class ValidSQL : IValidSQL {

		public List<Valid> GetByGuid(string guid) {
			string sql = @"
				SELECT id, guid, module_id, value FROM valid WHERE guid = @guid
			";
			return DataSource.QueryMany<Valid>(sql, new { guid });
		}

		public long Save(Valid valid) {
			return DataSource.Save(valid);
		}
	}
}
