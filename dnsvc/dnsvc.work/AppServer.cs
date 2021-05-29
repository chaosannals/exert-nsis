using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Resources;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using WebSocketSharp.Server;

namespace DnSvc.Work
{
    public class AppServer
    {
        public static Dictionary<string, string> MIME = new Dictionary<string, string>() {
            { ".html", "text/html" },
            { ".css", "text/css" },
            { ".js", "application/x-javascript" },
            { ".json", "application/json" },
            { ".jpg", "image/jpeg" },
            { ".png", "image/png" },
            { ".ico", "image/x-icon" },
            { ".ttf", "application/octet-stream" },
            { ".woff", "application/octet-stream" },
        };

        public static string Location { get { return AppDomain.CurrentDomain.BaseDirectory; } }
        public static string Here { get { return Path.GetDirectoryName(Location); } }
        public static string WwwDir { get { return Path.Combine(Here, "www"); } }
        public static string WwwZip { get { return Path.Combine(Here, "www.zip"); } }

        public HttpServer Server { get; private set; }

        public AppServer()
        {
            int port = Setting.GetInt("service", "port", 12345);
            Server = new HttpServer(IPAddress.Any, port);
            // .net framework 3.5 低版本无此枚举，强转换，TLS 1.2 的枚举。
            Server.SslConfiguration.EnabledSslProtocols = (SslProtocols)0xC00;
            ResourceManager manager = new ResourceManager("DnSvc.Work.Properties.Resources", typeof(AppServer).Assembly);
            Server.SslConfiguration.ServerCertificate = new X509Certificate2(manager.GetObject("dnsvc.pfx") as byte[]);
            Server.AddWebSocketService<Dispatcher>("/api");
            Server.OnGet += new EventHandler<HttpRequestEventArgs>(OutputWww);
        }

        public void Start()
        {
            if (Directory.Exists(WwwDir))
            {
                Directory.Delete(WwwDir, true);
            }
            UnzipWww();
            Server.Start();
            "服务开始".Log();
        }

        public void Stop()
        {
            Server.Stop();
            "服务停止".Log();
            LogExtends.Finally();
        }

        /// <summary>
        /// 解压前端。
        /// </summary>
        private void UnzipWww()
        {
            ZipStorer zip = ZipStorer.Open(WwwZip, FileAccess.Read);
            List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();
            foreach (ZipStorer.ZipFileEntry entry in dir)
            {
                string p = Path.Combine(WwwDir, entry.FilenameInZip);
                string d = Path.GetDirectoryName(p);
                if (!Directory.Exists(d))
                {
                    Directory.CreateDirectory(d);
                }
                zip.ExtractFile(entry, p);
            }
            zip.Close();
            "{0} 解压到：{1}".Log(WwwZip, WwwDir);
        }

        /// <summary>
        /// 输出前端。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputWww(object sender, HttpRequestEventArgs e)
        {
            string location = e.Request.Url.AbsolutePath.Trim('/').Replace('/', '\\');
            string filePath = Path.Combine(WwwDir, location);
            string suffix = Path.GetExtension(filePath).ToLower();
            if (!File.Exists(filePath) || !MIME.ContainsKey(suffix))
            {
                filePath = Path.Combine(WwwDir, "index.html");
                suffix = ".html";
            }
            byte[] contents = File.ReadAllBytes(filePath);
            e.Response.ContentType = MIME[suffix];
            e.Response.ContentLength64 = contents.Length;
            e.Response.StatusCode = 200;
            e.Response.Close(contents, true);
        }
    }
}
