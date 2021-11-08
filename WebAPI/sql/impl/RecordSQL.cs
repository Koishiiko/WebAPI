using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
	public class RecordSQL : IRecordSQL {

		private IDataSource dataSource { get; }

		public RecordSQL(IDataSource dataSource) {
			this.dataSource = dataSource;
		}

		public List<Record> GetAll() {
			string sql = @"SELECT id, record_id, name FROM record";
			return dataSource.QueryMany<Record>(sql);
		}

		public Record GetByRecordId(int id) {
			string sql = @"SELECT id, record_id, name FROM record WHERE record_id = @id";
			return dataSource.QueryOne<Record>(sql, new { id = id });
		}
	}
}
