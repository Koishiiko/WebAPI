using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.utils {
    /// <summary>
    /// 配置信息工具类
    /// 获取appsettings.json中WebAPISettings Section的配置
    ///
    /// 将IOptions<WebAPISettings>注入获取配置简化成静态类调用
    /// 但运行时配置发生变化 仍然需要通过注入IOptionsSnapShot获取新的配置
    /// 或重新调用AppSettings.Initial()更新配置
    /// </summary>
    public static class AppSettings {

        private static WebAPISettings settings;

        public static string MSSQLString => settings.MSSQLString;
        public static string LogConfigPath => settings.LogConfigPath;
        public static string FolderPath => settings.FolderPath;
        public static string ImagePath => settings.ImagePath;
        public static string ReportTemplatePath => settings.ReportTemplatePath;

        public static void Initial(WebAPISettings settings) {
            AppSettings.settings = settings;
        }
    }
}
