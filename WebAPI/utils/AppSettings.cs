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

        public static string JWTSecret => settings.JWTSecret;
        public static string SwitchConnectionString => settings.SwitchConnectionString;
        public static string ViewConnectionString => settings.ViewConnectionString;
        public static string LogConfigPath => settings.LogConfigPath;
        public static string FolderPath => settings.FolderPath;
        public static string ImagePath => settings.ImagePath;
        public static string ReportTemplatePath => settings.ReportTemplatePath;

        public static void Initial(WebAPISettings settings) {
            AppSettings.settings = settings;
        }
    }

    public class WebAPISettings {

        public string JWTSecret { get; set; }
        public string SwitchConnectionString { get; set; }
        public string ViewConnectionString { get; set; }
        public string LogConfigPath { get; set; }
        public string FolderPath { get; set; }
        public string ImagePath { get; set; }
        public string ReportTemplatePath { get; set; }
    }
}
