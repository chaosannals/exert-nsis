using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DnSvc
{
    public class AppManager
    {
        public AppDomain Domain { get; private set; }
        public AppDomainSetup Setup { get; private set; }
        public string FriendlyName { get; private set; }

        public AppManager(string fname, string name="app")
        {
            Setup = new AppDomainSetup();
            Setup.ApplicationName = name;
            Setup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            Setup.PrivateBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "private");
            Setup.CachePath = Setup.ApplicationBase;
            Setup.ShadowCopyFiles = "true";
            Setup.ShadowCopyDirectories = Setup.ApplicationBase;
            FriendlyName = fname;
        }

        public void InitDomain()
        {
            Domain = AppDomain.CreateDomain(FriendlyName, null, Setup);
            Domain.UnhandledException += (sender, e) =>
            {
                "捕获了漏掉的异常".Log();
                e.ExceptionObject.ToString().Log();
            };
            Domain.ProcessExit += (sender, e) => {
                LogExtends.Finally();
            };
        }
    }
}
