# .NET Framework中使用UEditor.Core

在.NET Framework中使用UEditor.Core，与.Net Core中基本类似，下面我主要列一下不同点，有需要的可以去下载源代码，直接看源代码中的示例：

## 区别一：配置

在.Net Framework中，`Startup.cs`中的启动代码需要移动到`Global.asax.cs`中：

```csharp
 public class MvcApplication : System.Web.HttpApplication
 {
     protected void Application_Start()
     {
         AreaRegistration.RegisterAllAreas();
         FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
         RouteConfig.RegisterRoutes(RouteTable.Routes);
         BundleConfig.RegisterBundles(BundleTable.Bundles);
         
         UEditorMvcExtension.AddUEditorService(); // 看这里^_^
     }
 }
```

## 区别二：静态资源文件无需额外配置

在.NET Framework中，服务器会默认根据文件的物理路径去读取文件夹中的内容，所以不需要像.NET Core中那样额外配置静态资源文件夹

## 区别三：`UEditorService`的使用方式

在`.NET Core`中，我们可以使用到MVC自带的依赖注入功能来注入`UEditorService`，但是`.NET Framework`的MVC中并没有默认的依赖注入容器，所以我们在`Controller`中使用时，方式略有调整，示例代码如下：

```csharp
public class UEditorController : Controller
{
    public ContentResult Upload()
    {
       var response = UEditorService.Instance.UploadAndGetResponse(HttpContext.ApplicationInstance.Context);
            return Content(response.Result, response.ContentType);
    }
}
```

