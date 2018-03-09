using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UEditor.Standard;

namespace UEditor.Framework
{
   public static class UEditorMvcExtension2
    {
        public static void AddUEditorService(string configFileRelativePath = "ueditor.json", bool isCacheConfig = true, string basePath = "")
        {
            Config.ConfigFile = configFileRelativePath;
            Config.NoCache = isCacheConfig;
            Config.WebRootPath = basePath;
        }

        public static IHttpContextHander GetHttpContext(this HttpContext httpContext)
        {
           return new HttpContextHander(httpContext);
        }
    }
}
