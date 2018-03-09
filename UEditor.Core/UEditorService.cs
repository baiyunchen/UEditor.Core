using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using UEditor.Standard;
using UEditor.Standard.Handlers;
//using UEditor.Core.Handlers;

namespace UEditor.Core
{
    public class UEditorService
    {
        public UEditorService(IHostingEnvironment env)
        {
            // .net core的名字起的比较怪而已，并不是我赋值赋错了
            if (string.IsNullOrWhiteSpace(Config.WebRootPath))
            {
                Config.WebRootPath = env.ContentRootPath;
            }

            Config.EnvName = env.EnvironmentName;
        }

        /// <summary>
        /// 上传并返回结果，已处理跨域Jsonp请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public UEditorResponse UploadAndGetResponse(IHttpContextHander context)
        {
            #region MyRegion
            //var action = context.Request.Query["action"];
            //object result;
            //if (AppConsts.Action.Config.Equals(action, StringComparison.OrdinalIgnoreCase))
            //{
            //    var configHandle = new ConfigHandler();
            //    result = configHandle.Process();
            //}
            //else
            //{
            //    var handle = HandelFactory.GetHandler(action, context);
            //    result = handle.Process();
            //}
            //string resultJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore
            //});
            //string contentType = "text/plain";

            //string jsonpCallback = context.Request.Query["callback"];

            //if (!string.IsNullOrWhiteSpace(jsonpCallback))
            //{
            //    contentType = "application/javascript";
            //    resultJson = string.Format("{0}({1});", jsonpCallback, resultJson);
            //    UEditorResponse response = new UEditorResponse(contentType, resultJson);
            //    return response;
            //}
            //else
            //{
            //    UEditorResponse response = new UEditorResponse(contentType, resultJson);
            //    return response;
            //} 
            #endregion
           return UEditorResponse.GetUEditorResponse(BaseUEditorService.Current.UploadAndGetResponse(context));
        }

        /// <summary>
        /// 单纯的上传并返回结果，未处理跨域Jsonp请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object Upload(IHttpContextHander context)
        {
            var action = context.QueryString("action");
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
