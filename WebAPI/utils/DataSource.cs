using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SqlSugar;
using System;
using System.Configuration;
using WebAPI.enums;

namespace WebAPI.utils {
    public static class DataSource {

        public static readonly SqlSugarScope DB;

        public static SqlSugarProvider Switch => DB.GetConnection(MesType.Switch);
        public static SqlSugarProvider View => DB.GetConnection(MesType.View);

        static DataSource() {
            DB = new SqlSugarScope(new List<ConnectionConfig> {
                new ConnectionConfig() {
                    ConfigId = MesType.Switch,
                    ConnectionString = AppSettings.SwitchConnectionString,
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    ConfigureExternalServices = new ConfigureExternalServices() {
                        EntityService = (t, column) => {
                            column.DbColumnName = CamelToSnake(column.DbColumnName);
                            if (column.DbColumnName == "id") {
                                column.IsPrimarykey = true;
                                column.IsIdentity = true;
                            }
                        },
                        EntityNameService = (t, info) => info.DbTableName = CamelToSnake(info.DbTableName)
                    }
                }, new ConnectionConfig() {
                    ConfigId = MesType.View,
                    ConnectionString = AppSettings.ViewConnectionString,
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    ConfigureExternalServices = new ConfigureExternalServices() {
                        EntityService = (t, column) => {
                            column.DbColumnName = CamelToSnake(column.DbColumnName);
                            if (column.DbColumnName == "id") {
                                column.IsPrimarykey = true;
                                column.IsIdentity = true;
                            }
                        },
                        EntityNameService = (t, info) => info.DbTableName = CamelToSnake(info.DbTableName)
                    }
                }
            });

            DB.Aop.OnLogExecuting = (sql, args) => {
                //Console.Write(sql);
            };
        }

        public static string CamelToSnake(string camelString) {
            return string.IsNullOrEmpty(camelString) ? string.Empty : Regex.Replace(camelString, @"(?<!_|^)[A-Z](?=[a-z])|(?<!_|^|[A-Z])[A-Z]", "_$0").ToLower();
        }

        public static SqlSugarProvider GetDB(MesType type) => DB.GetConnection(type);

        public static List<T> QueryMany<T>(string sql, object args = null, MesType type = MesType.Switch) {
            return DB.GetConnection(type).Ado.SqlQuery<T>(sql, args).ToList();
        }

        public static T QueryOne<T>(string sql, object args = null, MesType type = MesType.Switch) {
            return DB.GetConnection(type).Ado.SqlQuerySingle<T>(sql, args);
        }

        public static int Save<T>(T entity, MesType type = MesType.Switch) where T : class, new() {
            return DB.GetConnection(type).Insertable(entity).ExecuteReturnIdentity();
        }

        public static int Save<T>(List<T> list, MesType type = MesType.Switch) where T : class, new() {
            return DB.GetConnection(type).Insertable(list).ExecuteCommand();
        }

        public static int Update<T>(T entity, MesType type = MesType.Switch) where T : class, new() {
            return DB.GetConnection(type).Updateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommand();
        }

        public static int Delete(string sql, object args, MesType type = MesType.Switch) {
            return DB.GetConnection(type).Ado.ExecuteCommand(sql, args);
        }
    }
}
