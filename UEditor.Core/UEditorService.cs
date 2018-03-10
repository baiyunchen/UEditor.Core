#if NETSTANDARD2_0
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
#endif
#if NET35
using System.Web;
#endif
using Newtonsoft.Json;
using System;
using UEditor.Core.Handlers;

namespace UEditor.Core
{
    public class UEditorService
    {
#if NETSTANDARD2_0
        public UEditorService(IHostingEnvironment env)
        {
            // .net core的名字起的比较怪而已，并不是我赋值赋错了
            if (string.IsNullOrWhiteSpace(Config.WebRootPath))
            {
                Config.WebRootPath = env.ContentRootPath;
            }

            Config.EnvName = env.EnvironmentName;
        }
#endif
#if NET35
        private UEditorService()
        {

        }

        private static UEditorService _instance;

        public static UEditorService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UEditorService();
                }
                return _instance;
            }
        }
#endif
        /// <summary>
        /// 上传并返回结果，已处理跨域Jsonp请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public UEditorResponse UploadAndGetResponse(HttpContext context)
        {
#if NETSTANDARD2_0
             var action = context.Request.Query["action"];
#endif
#if NET35
            var action = context.Request.QueryString["action"];
#endif

            object result;
            if (AppConsts.Action.Config.Equals(action, StringComparison.OrdinalIgnoreCase))
            {
                var configHandle = new ConfigHandler();
                result = configHandle.Process();
            }
            else
            {
                var handle = HandelFactory.GetHandler(action, context);
                result = handle.Process();
            }
            string resultJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            string contentType = "text/plain";
#if NETSTANDARD2_0
             string jsonpCallback = context.Request.Query["callback"];
#endif
#if NET35
            string jsonpCallback = context.Request.QueryString["callback"];
#endif
            if (!jsonpCallback.IsNullOrWhiteSpace())
            {
                contentType = "application/javascript";
                resultJson = string.Format("{0}({1});", jsonpCallback, resultJson);
                UEditorResponse response = new UEditorResponse(contentType, resultJson);
                return response;
            }
            else
            {
                UEditorResponse response = new UEditorResponse(contentType, resultJson);
                return response;
            }
        }

        /// <summary>
        /// 单纯的上传并返回结果，未处理跨域Jsonp请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object Upload(HttpContext context)
        {
#if NETSTANDARD2_0
             var action = context.Request.Query["action"];
#endif
#if NET35
            var action = context.Request.QueryString["action"];
#endif 
            object result;
            if (AppConsts.Action.Config.Equals(action, StringComparison.OrdinalIgnoreCase))
            {
                result = new ConfigHandler();
            }
            else
            {
                var handle = HandelFactory.GetHandler(action, context);
                result = handle.Process();
            }
            return result;
        }
    }
}
