using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using SqlSugar;

namespace WebAPI.utils {
    public class DataSource {

        private static SqlSugarScope db;

        static DataSource() {
            db = new SqlSugarScope(new ConnectionConfig() {
                ConnectionString = AppSettings.MSSQLString,
                DbType = SqlSugar.DbType.SqlServer,
                IsAutoCloseConnection = true,
                ConfigureExternalServices = new ConfigureExternalServices() {
                    EntityService = (t, column) => {
                        // SqlSugar在更新操作时没有提供属性名的映射(驼峰转下划线)
                        // 所以需要手动映射
                        column.DbColumnName = Regex.Replace(column.DbColumnName, "(?<!_|^)[A-Z]", "_$0");
                    }
                }
            });
        }

        public static List<T> QueryMany<T>(string sql, object args = null) {
            return db.Ado.SqlQuery<T>(sql, args).ToList();
        }

        public static T QueryOne<T>(string sql, object args = null) {
            return db.Ado.SqlQuerySingle<T>(sql, args);
        }

        public static long Save<T>(T entity) where T : class, new() {
            return db.Insertable(entity).ExecuteReturnIdentity();
        }

        public static int Save<T>(List<T> list) where T : class, new() {
            return db.Insertable(list).ExecuteCommand();
        }

        public static bool Update<T>(T entity) where T : class, new() {
            return db.Updateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandHasChange();
        }

        public static int Delete(string sql, object args) {
            return db.Ado.ExecuteCommand(sql, args);
        }
    }
}
