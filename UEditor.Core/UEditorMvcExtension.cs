using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace UEditor.Core
{
    public static class UEditorMvcExtension
    {
        /// <summary>
        /// 添加UEditor后端服务
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configFileRelativePath">配置文件相对路径</param>
        /// <param name="isCacheConfig">是否缓存配置文件</param>
        /// <param name="basePath">配置文件、文件存储路径等各种路径的根目录，默认为Web项目的根目录</param>
        public static void AddUEditorService(this IServiceCollection services,
            string configFileRelativePath = "ueditor.json",
            bool isCacheConfig = true,
            string basePath = "")
        {

            Config.ConfigFile = configFileRelativePath;
            Config.NoCache = isCacheConfig;
            Config.WebRootPath = basePath;

            services.TryAddSingleton<UEditorService>();
        }
    }
}
