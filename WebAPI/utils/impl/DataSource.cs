using System.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;

namespace WebAPI.utils {
	public class DataSource : IDataSource {

		private string _connectionString;

		public DataSource(IConfiguration configuration) {
			_connectionString = configuration.GetConnectionString("MSSQLString");
		}

		public IDbConnection GetConnection() {
			return new SqlConnection(_connectionString);
		}

		public List<T> QueryMany<T>(string sql, object args) {
			using (var conn = GetConnection()) {
				return conn.Query<T>(sql, args).ToList();
			}
		}

		public T QueryOne<T>(string sql, object args) {
			using (var conn = GetConnection()) {
				return conn.QuerySingleOrDefault<T>(sql, args);
			}
		}

		int IDataSource.Count(string sql, object args) {
			using (var conn = GetConnection()) {
				return conn.QuerySingleOrDefault<int>(sql, args);
			}
		}

		public long Save<T>(T entity) where T : class {
			using (var conn = GetConnection()) {
				return conn.Insert<T>(entity);
			}
		}

		public bool Update<T>(T entity) where T : class{
			using (var conn = GetConnection()) {
				return conn.Update<T>(entity);
			}
		}

		public int Delete(string sql, object args) {
			using (var conn = GetConnection()) {
				return conn.Execute(sql, args);
			}
		}
	}
}
