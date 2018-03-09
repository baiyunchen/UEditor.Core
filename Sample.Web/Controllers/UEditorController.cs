using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UEditor.Core;

namespace Sample.Web.Controllers
{
    public class UEditorController : Controller
    {
        private readonly UEditorService _ueditorService;
        public UEditorController(UEditorService ueditorService)
        {
            this._ueditorService = ueditorService;
        }

        [HttpGet, HttpPost]
        public ContentResult Upload()
        {
            var response = _ueditorService.UploadAndGetResponse(HttpContext.GetHttpContext());
            return Content(response.Result, response.ContentType);
        }
    }
}