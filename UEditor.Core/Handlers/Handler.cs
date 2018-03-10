#if NETSTANDARD2_0
using Microsoft.AspNetCore.Http;
#endif

#if NET35
using System.Web;
#endif

namespace UEditor.Core.Handlers
{
    public abstract class Handler
    {
        public Handler(HttpContext context)
        {
            this.Request = context.Request;
            this.Response = context.Response;
            this.Context = context;
        }

        public abstract UEditorResult Process();

        public HttpRequest Request { get; private set; }
        public HttpResponse Response { get; private set; }
        public HttpContext Context { get; private set; }
    }
}