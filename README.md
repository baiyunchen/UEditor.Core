# UEditor.Core
# 示例代码
- [传统多页应（MPA）用中使用UEditor.Core](https://github.com/baiyunchen/UEditor.Core/tree/master/Sample.Web)
- [API中使用UEditor.Core](https://github.com/baiyunchen/UEditor.Core/tree/master/Sample.Mvc)
- [.NET Framework MVC 中使用UEditor.Core](https://github.com/baiyunchen/UEditor.Core/tree/master/Sample.Mvc)

其他示例正在完善中，后续示例代码将实现以下功能：

- 跨域使用UEditor

**敬请期待！！！**

# 使用方法

> 以下示例均为.Net Core下的使用方法，.Net Framework下的使用说明文档正在更新中，敬请期待

对于在传统MPA应用和API中使用UEditor,分别有详细的文档，请参阅：
- [传统多页应（MPA）用中使用UEditor](Docs/传统的使用方式.md)
- [API中使用UEditor（待完善）](Docs/API的使用方式.md)
- 跨域使用UEditor（待完善）
- UEditor.Core可配置项
- UEditor.Core多环境

# 安装
> 建议从从nuget安装

- 方式1：可以直接在Nuget中搜索UEditor.Core并安装

- 方式2：通过命令行安装
```
Install-Package UEditor.Core
```

# 配置
## 可选配置项

在注入Service时，可以支持一些可选的配置，具体参数如下：

### configFileRelativePath
后端配置文件的相对路径，默认值为`ueditor.json`,即项目根目录的`ueditor.json`文件，这个文件是从UEditor官方提供的.NET版本下载包中的`utf8-net\net\config.json`复制过来的
### isCacheConfig
是否缓存配置文件，默认值为true。当设置为不缓存时，每次都会从文件中读取配置文件；当设置为缓存时，则第一次从配置文件中读取，以后都从内存中读取
### basePath
相对路径的根目录，默认值为项目的根目录，即`env.ContentRootPath`。系统中的后端配置文件、各种上传的路径都是基于该地址去计算其实际地址的。

> **特别注意：**

> 在修改`basePath`后，一定要注意调整后端配置文件中的`imageUrlPrefix`、`scrawlUrlPrefix`等等各种文件访问路径的前缀，否则可能出现文件可以上传，但是前端编辑器中总显示不出来的问题。

## 配置项使用方式
配置项需要在注入Service时设置，示例代码如下：
```
public void ConfigureServices(IServiceCollection services)
{
   services.AddUEditorService(configFileRelativePath: "config.json",
       isCacheConfig: false,
       basePath: "C:/basepath");
   services.AddMvc();
}
```
# 特别感谢
  优秀的开源项目离不开大家的支持，非常感谢为以下为本项目提供好的建议或PR的朋友：
  - [wtujvk](https://github.com/wtujvk)

## 大功告成，祝你大吉大利，今晚吃鸡
