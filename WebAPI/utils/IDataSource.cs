using System.Collections.Generic;
using System.Data;

namespace WebAPI.utils {
	public interface IDataSource {

		IDbConnection GetConnection();

		List<T> QueryMany<T>(string sql, object args = null);

		T QueryOne<T>(string sql, object args = null);

		int Count(string sql, object args = null);

		long Save<T>(T entity) where T : class;

		bool Update<T>(T entity) where T : class;

		int Delete(string sql, object args);
	}
}
