

namespace UEditor.Standard.Handlers
{
    public abstract class Handler
    {
        public Handler(IHttpContextHander context)
        {
            //this.Request = context.Request;
            //this.Response = context.Response;
            this.Context = context;
        }

        public abstract UEditorResult Process();

        //public HttpRequest Request { get; private set; }
        //public HttpResponse Response { get; private set; }
        public IHttpContextHander Context { get; private set; }
    }
}