
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UEditor.Standard.Handlers
{
    public class HandelFactory
    {
        #region 扩展添加action
        private static Dictionary<string, Handler> dictionary = new Dictionary<string, Handler>();

        public static void AddHander<T>(string key, T handler) where T : Handler
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException();
            }
            if (Enumerable.Contains<string>(AppConsts.Actions, key))
            {
                throw new ArgumentException("已有的action不需要添加");
            }
            if (dictionary.ContainsKey(key))
            {
                throw new ArgumentException("key已存在！");
            }
            dictionary.Add(key, handler);
        } 
        #endregion

        public static Handler GetHandler(string action, IHttpContextHander context)
        {
            switch (action)
            {
                case AppConsts.Action.UploadImage:
                    return new UploadHandler(context, new UploadConfig
                    {
                        AllowExtensions = Config.GetStringList("imageAllowFiles"),
                        PathFormat = Config.GetString("imagePathFormat"),
                        SizeLimit = Config.GetInt("imageMaxSize"),
                        UploadFieldName = Config.GetString("imageFieldName")
                    });
                case AppConsts.Action.UploadScrawl:
                    return new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = new string[] { ".png" },
                        PathFormat = Config.GetString("scrawlPathFormat"),
                        SizeLimit = Config.GetInt("scrawlMaxSize"),
                        UploadFieldName = Config.GetString("scrawlFieldName"),
                        Base64 = true,
                        Base64Filename = "scrawl.png"
                    });
                case AppConsts.Action.UploadVideo:
                    return new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = Config.GetStringList("videoAllowFiles"),
                        PathFormat = Config.GetString("videoPathFormat"),
                        SizeLimit = Config.GetInt("videoMaxSize"),
                        UploadFieldName = Config.GetString("videoFieldName")
                    });
                case AppConsts.Action.UploadFile:
                    return new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = Config.GetStringList("fileAllowFiles"),
                        PathFormat = Config.GetString("filePathFormat"),
                        SizeLimit = Config.GetInt("fileMaxSize"),
                        UploadFieldName = Config.GetString("fileFieldName")
                    });

                case AppConsts.Action.ListImage:
                    return new ListFileManager(context, Config.GetString("imageManagerListPath"), Config.GetStringList("imageManagerAllowFiles"));
                case AppConsts.Action.ListFile:
                    return new ListFileManager(context, Config.GetString("fileManagerListPath"), Config.GetStringList("fileManagerAllowFiles"));
                case AppConsts.Action.CatchImage:
                    return new CrawlerHandler(context);
                default:
                    if (dictionary.ContainsKey(action))
                    {
                        return dictionary[action];
                    }
                    return new NotSupportedHandler(context);
            }
        }
    }
}
