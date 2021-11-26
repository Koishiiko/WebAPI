using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.po;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class RecordSQL : IRecordSQL {


		public List<Record> GetAll() {
			string sql = @"SELECT id, record_id, name FROM record";
			return DataSource.QueryMany<Record>(sql);
		}

		public Record GetByRecordId(int id) {
			string sql = @"SELECT id, record_id, name FROM record WHERE record_id = @id";
			return DataSource.QueryOne<Record>(sql, new { id });
		}
	}
}
