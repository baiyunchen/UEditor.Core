# .NET Core中使用UEditor.Core
## 引用并配置
按照说明中的方法，从Nuget引用`UEditor.Core`类库，并在Startup.cs中的ConfigureServices方法中，加入以下代码：
```
services.AddUEditorService();
```
AddUEditorService方法中，可以传一些配置参数，这些配置均为可选的，具体配置可以参考文档首页[配置段落](https://github.com/baiyunchen/UEditor.Core#%E9%85%8D%E7%BD%AE)的说明。

## 配置静态资源文件夹

由于.Net Core默认只会从wwwroot目录加载静态文件，其他文件夹的静态文件无法正常访问。而我们希望将图片上传到网站根目录的upload文件夹下，所以需要额外在`Startup.cs`类的Configure方法中，增加如下代码：

```
app.UseStaticFiles(new StaticFileOptions
{
   FileProvider = new PhysicalFileProvider(
       Path.Combine(Directory.GetCurrentDirectory(), "upload")),
   RequestPath = "/upload",
   OnPrepareResponse = ctx =>
   {
       ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=36000");
   }
});
```
然后在项目的根目录，创建`upload`文件夹（这里不创建会报错）。

## 添加后端配置文件

接下来，我们需要将ueditor后端的`config.js` **改名** 为`ueditor.json`添加到项目根目录。

> 这里的配置文件可以在ueditor的下载包中，net文件夹下面找到，目前版本的具体路径如下：
`ueditor1_4_3_3-utf8-net\utf8-net\net\config.json`

如果你懒得下载，也可以在本项目GitHub的Sample.Web下面找到`ueditor.json`文件，然后粘贴到你项目的根目录下。

如果你是从UEditor的下载包中复制的该文件，需要全局将该文件中的`/ueditor/net/`替换为`/`

## 创建后端接口
创建一个UEditorController,并添加如下代码：
```csharp
public class UEditorController : Controller
{
    private readonly UEditorService _ueditorService;
    public UEditorController(UEditorService ueditorService)
    {
        this._ueditorService = ueditorService;
    }

	//如果是API，可以按MVC的方式特别指定一下API的URI
    [HttpGet, HttpPost]   
    public ContentResult Upload()
    {
        var response = _ueditorService.UploadAndGetResponse(HttpContext);
        return Content(response.Result, response.ContentType);
    }
}
```

## 跨域配置

如果你的UEditor所在网站的地址和上面创建的接口地址没有在同一个域名下，那么记得要配置跨域哦！

具体怎么配置允许跨域请移步：https://docs.microsoft.com/zh-cn/aspnet/core/security/cors

## 引入ueditor并配置

从ueditor[官网下载最新版本](http://ueditor.baidu.com/website/download.html#ueditor)的压缩包，并放到项目的wwwroot下面的lib文件夹中，然后在你需要的使用UEditor的页面或全局引入`ueditor.config.js`和`ueditor.all.min.js`文件。

打开`ueditor.config.js`文件，将其中的`serverUrl`项改为`UEditorController`中`Upload`方法对应的地址(没有改路由的话应该是：`UEditor/Upload`)。

**注意：** 如果你的UEditor编辑器所在的页面地址和上面写的Controller所对应的接口地址如果没有在一个站点下面，那么serverUrl应该写成带域名和端口号的全路径（http://youhost/UEditor/Upload）。

## 开始使用UEditor
这部分如果遇到问题，请参考UEditor的官方文档[http://fex.baidu.com/ueditor/](http://fex.baidu.com/ueditor/)。

在你需要使用UEditor的HTML代码中，添加如下代码：
```
<script id="container" name="content" type="text/plain">
    欢迎使用Ueditor.Core
</script>
```
然后在页面的最后添加如下JS代码：
```
<script type="text/javascript">
    var ue = UE.getEditor('container', {
        initialFrameHeight: 500
    });
</script>
```
