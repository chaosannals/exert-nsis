using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace DnSvc
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                "捕获了漏掉的异常".Log();
                e.ExceptionObject.ToString().Log();
            };
            AppDomain.CurrentDomain.ProcessExit += (sender, e) => {
                LogExtends.Finally();
            };
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new DotnetService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
