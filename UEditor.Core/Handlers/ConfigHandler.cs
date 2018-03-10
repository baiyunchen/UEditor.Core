using Newtonsoft.Json.Linq;

namespace UEditor.Core.Handlers
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public class ConfigHandler
    {
        public JObject Process()
        {
            return Config.Items;
        }
    }
}