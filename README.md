# UEditor.Core
# Demo
目前正在完善中，后续Demo将实现以下功能：
- API中使用UEditor.Core
- 传统多页应（MPA）用中使用UEditor
- 单页应用（SPA）中使用UEditor

**敬请期待！！！**
# 使用方法
## 从nuget安装包：
方式1：可以直接在Nuget中搜索UEditor.Core并安装
方式2：通过命令行安装
```
Install-Package UEditor.Core -Version 1.0.0
```
## 在启动时注入相关服务
在Startup.cs中的ConfigureServices方法中，加入以下代码：
```
 services.AddUEditorService("ueditor.json",true);
```
该方法中，第一个参数为ueditor的后端配置文件路径，默认值为`ueditor.json`，第二个参数为是否缓存配置，默认为`true`,当缓存配置时，默认仅会从后端配置文件中加载一次数据，之后都会从缓存中读取。

这两个参数均为可选参数，你完全可以写成：
```
 services.AddUEditorService();
```
## 添加配置文件
从UEditor中或本项目的Demo项目中，复制ueditor.json文件，复制到你的项目中，最终只需要将这个文件的相对路径在Startup.cs中注入服务时指定正确即可。

如果你看不懂上面的这句话，那就把这个项目放到你Web或者API项目的根目录（与`appsettings.json`同级即可），然后上面Startup.cs中的代码写成下面这样：
```
 services.AddUEditorService();
```
最后，需要按照自己的实际情况，修改一下ueditor.json文件，具体怎么改，请参考这里：http://fex.baidu.com/ueditor/#server-config
## 添加控制器
添加一个UEditorController，然后搞成下面这样子：
```
[Route("api/ueditor")]
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
        var response = _ueditorService.UploadAndGetResponse(HttpContext);
        return Content(response.Result, response.ContentType);
    }
}
```
到此，我们的后端代码就全部搞定了

需要注意的是，如果你的UEditor所在网站的地址和这个API的地址没有在同一个域名下，那么记得要配置跨域哦~

具体怎么配置允许跨域请移步：https://docs.microsoft.com/zh-cn/aspnet/core/security/cors
## 修改前端UEditor的配置
UEditor前端的配置位于根目录的ueditor.config.js中，我们需要在这个文件中找到`serverUrl`节点，然后指定刚添加的Controller中的API地址，从而告诉UEditor图片之类的文件要往哪里上传。
大概的配置代码如下：
```
serverUrl: "http://你的域名+端口/api/ueditor"
```

## 大功告成，祝你大吉大利，今晚吃鸡

