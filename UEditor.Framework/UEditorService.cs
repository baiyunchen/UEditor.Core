using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UEditor.Standard;
using UEditor.Standard.Handlers;

namespace UEditor.Framework
{
    public class UEditorService: ThreadSafeLazyBaseSingleton<UEditorService>
    {
        public UEditorService()
        {
            if (string.IsNullOrWhiteSpace(Config.WebRootPath))
            {
                Config.WebRootPath = HttpContext.Current.Server.MapPath("~/");
            }
            string str = string.Empty;
#if DEBUG
            str = "DEBUG";
#endif
#if RELEASE
            str="RELEASE";
#endif
            Config.EnvName = str;
        }

        public object Upload(HttpContext context)
        {
            string str = context.Request.QueryString["action"];
            if ("config".Equals(str, StringComparison.OrdinalIgnoreCase))
            {
                return new ConfigHandler();
            }
            return HandelFactory.GetHandler(str, context.GetHttpContext()).Process();
        }

        public BaseUEditorResponse UploadAndGetResponse(HttpContext context)
        {
          return BaseUEditorService.Current.UploadAndGetResponse(context.GetHttpContext());
        }
    }
}

