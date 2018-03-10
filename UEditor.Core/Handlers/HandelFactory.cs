#if NETSTANDARD2_0
using Microsoft.AspNetCore.Http;
#endif
#if NET35
using System.Web;
#endif
using System;
using System.Collections.Generic;
using System.Text;

namespace UEditor.Core.Handlers
{
    public class HandelFactory
    {
        public static Handler GetHandler(string action, HttpContext context)
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
                    return new NotSupportedHandler(context);
            }
        }
    }
}
