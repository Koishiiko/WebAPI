using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class ValidSQL : IValidSQL {

		private IDataSource dataSource { get; }

		public ValidSQL(IDataSource dataSource) {
			this.dataSource = dataSource;
		}

		public List<Valid> GetByGuid(string guid) {
			string sql = @"
				SELECT id, guid, module_id, value FROM valid WHERE guid = @guid
			";
			return dataSource.QueryMany<Valid>(sql, new { guid });
		}

		public long Save(Valid valid) {
			return dataSource.Save(valid);
		}
	}
}
