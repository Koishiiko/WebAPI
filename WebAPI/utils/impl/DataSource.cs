using System.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using SqlSugar;

namespace WebAPI.utils {
	public class DataSource : IDataSource {

		private string _connectionString;
		private SqlSugarScope db;

		public DataSource(IConfiguration configuration) {
			_connectionString = configuration.GetConnectionString("MSSQLString");
			db = new SqlSugarScope(new ConnectionConfig() {
				ConnectionString = _connectionString,
				DbType = SqlSugar.DbType.SqlServer,
				IsAutoCloseConnection = true
			});
		}

		public List<T> QueryMany<T>(string sql, object args) {
			return db.Ado.SqlQuery<T>(sql, args).ToList();
		}

		public T QueryOne<T>(string sql, object args) {
			return db.Ado.SqlQuerySingle<T>(sql, args);
		}

		int IDataSource.Count(string sql, object args) {
			return db.Ado.SqlQuerySingle<int>(sql, args);
		}

		public long Save<T>(T entity) where T : class, new() {
			return db.Insertable<T>(entity).ExecuteReturnIdentity();
		}

		public bool Update<T>(T entity) where T : class, new() {
			return db.Updateable<T>(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandHasChange();
		}

		public int Delete(string sql, object args) {
			return db.Ado.ExecuteCommand(sql, args);
		}
	}
}
