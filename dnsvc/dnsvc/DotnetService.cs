using System;
using System.ServiceProcess;

namespace DnSvc
{
    public partial class DotnetService : ServiceBase
    {
        public AppManager Manager { get; private set; }

        public DotnetService()
        {
            InitializeComponent();
            Manager = new AppManager("DnSvcDomain");
        }

        protected override void OnStart(string[] args)
        {
            "服务开始".Log();
            Manager.InitDomain();
        }

        protected override void OnStop()
        {

            "服务停止".Log();
            LogExtends.Finally();
        }
    }
}
