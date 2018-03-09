using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UEditor.Standard;

namespace UEditor.Framework
{
    public class HttpContextHander : IHttpContextHander
    {
        private HttpContext httpContext;
        public HttpContextHander(HttpContext _httpContext)
        {
            this.httpContext = _httpContext;
        }

        public IEnumerable<string> ArryForm(string key) =>
            this.httpContext.Request.Form.GetValues(key);

        public SimpleFormFile FormFile(string key)
        {
            var file = this.httpContext.Request.Files[key];
            return new SimpleFormFile
            {
                FileName = file.FileName,
                Length = file.ContentLength,
                Name = file.FileName,
                OpenReadStream = (() => file.InputStream)
            };
       }

        public byte[] FromBase64String(string key)
        {
            return Convert.FromBase64String(httpContext.Request.Form[key]);
        }
        public string QueryString(string key)
        {
            return httpContext.Request.QueryString[key];
        }
    }
}
