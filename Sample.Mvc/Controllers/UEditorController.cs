using System.Web.Mvc;
using UEditor.Core;

namespace Sample.Mvc.Controllers
{
    public class UEditorController : Controller
    {
        public ContentResult Upload()
        {
            var response = UEditorService.Instance.UploadAndGetResponse(HttpContext.ApplicationInstance.Context);
            return Content(response.Result, response.ContentType);
        }
    }
}