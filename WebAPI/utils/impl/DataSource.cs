using System.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using SqlSugar;
using System.Text.RegularExpressions;

namespace WebAPI.utils {
	public class DataSource : IDataSource {

		private readonly string _connectionString;
		private SqlSugarScope db;

		public DataSource(IConfiguration configuration) {
			_connectionString = configuration.GetConnectionString("MSSQLString");
			db = new SqlSugarScope(new ConnectionConfig() {
				ConnectionString = _connectionString,
				DbType = SqlSugar.DbType.SqlServer,
				IsAutoCloseConnection = true,
				ConfigureExternalServices = new ConfigureExternalServices() {
					EntityService = (t, column) => {
						// sqlSugar在更新操作时没有提供属性名的映射(驼峰转下划线)
						// 所以需要手动映射
						column.DbColumnName = Regex.Replace(column.DbColumnName, "(?<!_|^)[A-Z]", "_$0");
					}
				}
			});
		}

		public List<T> QueryMany<T>(string sql, object args) {
			return db.Ado.SqlQuery<T>(sql, args).ToList();
		}

		public T QueryOne<T>(string sql, object args) {
			return db.Ado.SqlQuerySingle<T>(sql, args);
		}

		public long Save<T>(T entity) where T : class, new() {
			return db.Insertable<T>(entity).ExecuteReturnIdentity();
		}

		public int Save<T>(List<T> list) where T : class, new() {
			return db.Insertable<T>(list).ExecuteCommand();
		}

		public bool Update<T>(T entity) where T : class, new() {
			return db.Updateable<T>(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandHasChange();
		}

		public int Delete(string sql, object args) {
			return db.Ado.ExecuteCommand(sql, args);
		}
	}
}
