using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionPlusDesktop.Core
{
    public class AppConfig
    {
        [Newtonsoft.Json.JsonProperty("APP_NAME")]
        public string APP_NAME { get; } = "SubscriptionPlus";

        [Newtonsoft.Json.JsonProperty("APP_FOLDER")]
        public string APP_FOLDER_PATH { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SubscriptionPlusDesktop");

        [Newtonsoft.Json.JsonProperty("CONFIG_USER_PATH")]
        public string CONFIG_SUBPLUSJSON_PATH { get; }

        [Newtonsoft.Json.JsonProperty("LOGS_PATH")]
        public string LOGS_PATH { get; }

        public AppConfig()
        {
            this.CONFIG_SUBPLUSJSON_PATH = Path.Combine(APP_FOLDER_PATH, "subs.json");
            this.LOGS_PATH = Path.Combine(APP_FOLDER_PATH, "Logs");
        }
    }

    public static class Config
    {
        public static AppConfig Current { get; } = new AppConfig();
    } //  Config.Current.AppName;
}
