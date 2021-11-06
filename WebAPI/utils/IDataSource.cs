using System.Collections.Generic;
using System.Data;

namespace WebAPI.utils {
	public interface IDataSource {

		List<T> QueryMany<T>(string sql, object args = null);

		T QueryOne<T>(string sql, object args = null);

		int Count(string sql, object args = null);

		long Save<T>(T entity) where T : class, new();

		bool Update<T>(T entity) where T : class, new();

		int Delete(string sql, object args);
	}
}
