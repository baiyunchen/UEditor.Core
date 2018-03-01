using Microsoft.AspNetCore.Http;

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