using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if NETSTANDARD2_0
using Microsoft.AspNetCore.Http;
#endif
#if NET35
using System.Web;
#endif

namespace UEditor.Core.Handlers
{
    /// <summary>
    /// FileManager 的摘要说明
    /// </summary>
    public class ListFileManager : Handler
    {
        enum ResultState
        {
            Success,
            InvalidParam,
            AuthorizError,
            IOError,
            PathNotFound
        }

        private int Start;
        private int Size;
        private int Total;
        private ResultState State;
        private String PathToList;
        private String[] FileList;
        private String[] SearchExtensions;

        public ListFileManager(HttpContext context, string pathToList, string[] searchExtensions)
            : base(context)
        {
            this.SearchExtensions = searchExtensions.Select(x => x.ToLower()).ToArray();
            this.PathToList = pathToList;
        }

        public override UEditorResult Process()
        {
            try
            {
#if NETSTANDARD2_0
                Start = string.IsNullOrWhiteSpace(Request.Query["start"]) ? 0 : Convert.ToInt32(Request.Query["start"]);
                Size = string.IsNullOrWhiteSpace(Request.Query["size"]) ? Config.GetInt("imageManagerListSize") : Convert.ToInt32(Request.Query["size"]);
#endif
#if NET35
                Start = Request["start"].IsNullOrWhiteSpace() ? 0 : Convert.ToInt32(Request["start"]);
                Size = Request["size"].IsNullOrWhiteSpace() ? Config.GetInt("imageManagerListSize") : Convert.ToInt32(Request["size"]);
#endif
            }
            catch (FormatException)
            {
                State = ResultState.InvalidParam;
                return WriteResult();
            }
            UEditorResult result;
            var buildingList = new List<String>();
            try
            {
                var localPath = Path.Combine(Config.WebRootPath, PathToList);
                buildingList.AddRange(Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
                    .Where(x => SearchExtensions.Contains(Path.GetExtension(x).ToLower()))
                    .Select(x => PathToList + x.Substring(localPath.Length).Replace("\\", "/")));
                Total = buildingList.Count;
                FileList = buildingList.OrderBy(x => x).Skip(Start).Take(Size).ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                State = ResultState.AuthorizError;
            }
            catch (DirectoryNotFoundException)
            {
                State = ResultState.PathNotFound;
            }
            catch (IOException)
            {
                State = ResultState.IOError;
            }
            finally
            {
                result = WriteResult();
            }

            return result;
        }

        private UEditorResult WriteResult()
        {
            return new UEditorResult
            {
                State = GetStateString(),
                List = FileList?.Select(x => new UEditorFileList { Url = x }),
                Start = Start,
                Size = Size,
                Total = Total
            };
        }

        private string GetStateString()
        {
            switch (State)
            {
                case ResultState.Success:
                    return "SUCCESS";
                case ResultState.InvalidParam:
                    return "参数不正确";
                case ResultState.PathNotFound:
                    return "路径不存在";
                case ResultState.AuthorizError:
                    return "文件系统权限不足";
                case ResultState.IOError:
                    return "文件系统读取错误";
            }
            return "未知错误";
        }
    }
}