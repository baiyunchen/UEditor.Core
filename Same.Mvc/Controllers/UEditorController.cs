using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UEditor.Framework;

namespace Same.Mvc.Controllers
{
    public class UEditorController : Controller
    {
        // GET: UEditor
       
            private readonly UEditorService _ueditorService= UEditorService.Current;
           

           // [HttpGet, HttpPost] **多个特性的问题： asp.net mvc不支持这种方式 .netcore就没问题
            public ContentResult Upload()
            {
                var response = _ueditorService.UploadAndGetResponse(System.Web.HttpContext.Current);
                return Content(response.Result, response.ContentType);
            }
        
    }
}