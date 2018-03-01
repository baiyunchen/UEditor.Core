using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace UEditor.Core
{
    public static class UEditorMvcExtension
    {
        public static void AddUEditorService(this IServiceCollection services, 
            string configFileRelativePath = "ueditor.json", 
            bool isCache = true)
        {

            Config.ConfigFile = configFileRelativePath;
            Config.NoCache = isCache;

            services.TryAddSingleton<UEditorService>();
        }
    }
}
