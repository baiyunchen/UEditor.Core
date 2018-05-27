using System;
using System.IO;
using System.Linq;
using System.Net;
#if NETSTANDARD2_0
using Microsoft.AspNetCore.Http;
#endif
#if NET35
using System.Web;
#endif
namespace UEditor.Core.Handlers
{
    public class CrawlerHandler : Handler
    {
        private string[] _sources;
        private Crawler[] _crawlers;
        public CrawlerHandler(HttpContext context) : base(context) { }

        public override UEditorResult Process()
        {
#if NETSTANDARD2_0            
            _sources = Request.Form["source[]"];

            //fixed bug:https://github.com/baiyunchen/UEditor.Core/pull/5
            if (_sources == null || _sources.Length == 0)
            {
                _sources = Request.Query["source[]"];
            }
#endif
#if NET35
            _sources = Request.Form.GetValues("source[]");

            //fixed bug:https://github.com/baiyunchen/UEditor.Core/pull/5
            if (_sources == null || _sources.Length == 0) 
            {
                _sources = Request.QueryString.GetValues("source[]");
            }
#endif
            if (_sources == null || _sources.Length == 0)
            {
                return new UEditorResult
                {
                    State = "参数错误：没有指定抓取源"
                };

            }
            _crawlers = _sources.Select(x => new Crawler(x).Fetch()).ToArray();
            return new UEditorResult
            {
                State = "SUCCESS",
                List = _crawlers.Select(x => new UEditorFileList
                {
                    State = x.State,
                    Source = x.SourceUrl,
                    Url = x.ServerUrl
                })
            };
        }
    }

    public class Crawler
    {
        public string SourceUrl { get; set; }
        public string ServerUrl { get; set; }
        public string State { get; set; }


        public Crawler(string sourceUrl)
        {
            this.SourceUrl = sourceUrl;
        }

        public Crawler Fetch()
        {
            if (!IsExternalIpAddress(this.SourceUrl))
            {
                State = "INVALID_URL";
                return this;
            }
            var request = WebRequest.Create(this.SourceUrl) as HttpWebRequest;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response != null && response.StatusCode != HttpStatusCode.OK)
                {
                    State = "Url returns " + response.StatusCode + ", " + response.StatusDescription;
                    return this;
                }
                if (response != null && response.ContentType.IndexOf("image", StringComparison.Ordinal) == -1)
                {
                    State = "Url is not an image";
                    return this;
                }
                var sourceUri = new Uri(this.SourceUrl);

                ServerUrl = PathFormatter.Format(Path.GetFileName(sourceUri.AbsolutePath), Config.GetString("catcherPathFormat"));

                var savePath = Path.Combine(Config.WebRootPath, ServerUrl);

                if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                }
                try
                {
                    if (response != null)
                    {
                        var stream = response.GetResponseStream();
                        var reader = new BinaryReader(stream);
                        byte[] bytes;
                        using (var ms = new MemoryStream())
                        {
                            byte[] buffer = new byte[4096];
                            int count;
                            while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                ms.Write(buffer, 0, count);
                            }
                            bytes = ms.ToArray();
                        }
                        File.WriteAllBytes(savePath, bytes);
                    }

                    State = "SUCCESS";
                }
                catch (Exception e)
                {
                    State = "抓取错误：" + e.Message;
                }
                return this;
            }
        }

        private bool IsExternalIpAddress(string url)
        {
            var uri = new Uri(url);
            switch (uri.HostNameType)
            {
                case UriHostNameType.Dns:
                    var ipHostEntry = Dns.GetHostEntry(uri.DnsSafeHost);
                    foreach (IPAddress ipAddress in ipHostEntry.AddressList)
                    {
                        byte[] ipBytes = ipAddress.GetAddressBytes();
                        if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            if (!IsPrivateIP(ipAddress))
                            {
                                return true;
                            }
                        }
                    }
                    break;

                case UriHostNameType.IPv4:
                    return !IsPrivateIP(IPAddress.Parse(uri.DnsSafeHost));
            }
            return false;
        }

        private bool IsPrivateIP(IPAddress myIpAddress)
        {
            if (IPAddress.IsLoopback(myIpAddress)) return true;
            if (myIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                byte[] ipBytes = myIpAddress.GetAddressBytes();
                // 10.0.0.0/24 
                if (ipBytes[0] == 10)
                {
                    return true;
                }
                // 172.16.0.0/16
                else if (ipBytes[0] == 172 && ipBytes[1] == 16)
                {
                    return true;
                }
                // 192.168.0.0/16
                else if (ipBytes[0] == 192 && ipBytes[1] == 168)
                {
                    return true;
                }
                // 169.254.0.0/16
                else if (ipBytes[0] == 169 && ipBytes[1] == 254)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
