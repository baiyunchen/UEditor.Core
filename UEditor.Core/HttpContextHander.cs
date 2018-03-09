using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using UEditor.Standard;

namespace UEditor.Core
{
    public class HttpContextHander : IHttpContextHander
    {
        HttpContext httpContext;
        public HttpContextHander(HttpContext _httpContext)
        {
            httpContext = _httpContext;
        }
        public IEnumerable<string> ArryForm(string key)
        {
           return httpContext.Request.Form[key];
        }

        public SimpleFormFile FormFile(string key)
        {
            var file= httpContext.Request.Form.Files[key];
            return new SimpleFormFile() { FileName = file.FileName, Length = file.Length, Name = file.Name,OpenReadStream=file.OpenReadStream };
        }

        //public string FormString(string key)
        //{
        //    httpContext.Request.Form[key];
        //}

        public byte[] FromBase64String(string key)
        {
            return Convert.FromBase64String(httpContext.Request.Form[key]);
        }

        public string QueryString(string key)
        {
            return httpContext.Request.Query[key];
        }

        //public string RequestQuery(string key)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
