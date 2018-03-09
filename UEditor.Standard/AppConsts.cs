using System;
using System.Collections.Generic;
using System.Text;

namespace UEditor.Standard
{
    public class AppConsts
    {
       
        public static string[] Actions = new string[] { "config", "uploadimage", "uploadscrawl", "uploadvideo", "uploadfile", "listimage", "listfile", "catchimage" };
       
        public partial class Action
        {
            // Fields
            public const string CatchImage = "catchimage";
            public const string Config = "config";
            public const string ListFile = "listfile";
            public const string ListImage = "listimage";
            public const string UploadFile = "uploadfile";
            public const string UploadImage = "uploadimage";
            public const string UploadScrawl = "uploadscrawl";
            public const string UploadVideo = "uploadvideo";
        }
    }
}
