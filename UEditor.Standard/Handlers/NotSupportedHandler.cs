

namespace UEditor.Standard.Handlers
{
    /// <summary>
    /// NotSupportedHandler 的摘要说明
    /// </summary>
    public class NotSupportedHandler : Handler
    {
        public NotSupportedHandler(IHttpContextHander context)
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