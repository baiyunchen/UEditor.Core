using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace UEditor.Core
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public static class Config
    {
        public static bool NoCache = true;

        private static JObject BuildItems()
        {
            var configExtension = Path.GetExtension(ConfigFile);
            var configFileName = ConfigFile.Substring(0, ConfigFile.Length - configExtension.Length);
            var evnConfig = $"{configFileName}.{Config.EnvName}{configExtension}";
            if (File.Exists(Path.Combine(WebRootPath, evnConfig)))
            {
                var json = File.ReadAllText(Path.Combine(WebRootPath, evnConfig));
                return JObject.Parse(json);
            }
            else
            {
                var configFilePath = Path.Combine(WebRootPath, ConfigFile);
                if (!File.Exists(configFilePath))
                {
                    throw new Exception("未找到UEditor配置文件，请检查！若有问题，请参阅文档：https://github.com/baiyunchen/UEditor.Core");
                }
                var json = File.ReadAllText(configFilePath);
                return JObject.Parse(json);
            }
        }

        public static JObject Items
        {
            get
            {
                if (NoCache || _Items == null)
                {
                    _Items = BuildItems();
                }
                return _Items;
            }
        }

        public static string EnvName { get; set; }

        public static string WebRootPath { get; set; }

        // public static string WwwRootPath { get; set; }

        public static string ConfigFile { set; get; } = "ueditor.json";

        private static JObject _Items;


        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }

        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }

        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }

        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}