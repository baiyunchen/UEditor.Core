#if NETSTANDARD2_0
using Microsoft.AspNetCore.Http;
#endif
#if NET35
using System.Web;
#endif

namespace UEditor.Core.Handlers
{
    /// <summary>
    /// NotSupportedHandler 的摘要说明
    /// </summary>
    public class NotSupportedHandler : Handler
    {
        public NotSupportedHandler(HttpContext context)
            : base(context)
        {
        }

        public override UEditorResult Process()
        {
            return new UEditorResult
            {
                State = "action 参数为空或者 action 不被支持。"
            };
        }
    }
}