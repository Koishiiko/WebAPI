using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SqlSugar;

namespace WebAPI.utils {
    /// <summary>
    /// 数据库操作工具类
    /// 
    /// 使用SqlSugar框架
    /// 除了使用定义的静态方法外
    /// 还可以通过DataSource.DB调用框架中提供的方法
    /// </summary>
    public static class DataSource {

        public static SqlSugarScope DB { get; }

        static DataSource() {
            DB = new SqlSugarScope(new ConnectionConfig() {
                ConnectionString = AppSettings.MSSQLString,
                DbType = DbType.SqlServer,
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
            return DB.Ado.SqlQuery<T>(sql, args).ToList();
        }

        public static T QueryOne<T>(string sql, object args = null) {
            return DB.Ado.SqlQuerySingle<T>(sql, args);
        }

        public static long Save<T>(T entity) where T : class, new() {
            return DB.Insertable(entity).ExecuteReturnIdentity();
        }

        public static int Save<T>(List<T> list) where T : class, new() {
            return DB.Insertable(list).ExecuteCommand();
        }

        public static bool Update<T>(T entity) where T : class, new() {
            return DB.Updateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandHasChange();
        }

        public static int Delete(string sql, object args) {
            return DB.Ado.ExecuteCommand(sql, args);
        }
    }
}
