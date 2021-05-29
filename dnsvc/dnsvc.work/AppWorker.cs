using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnSvc.Work
{
    public class AppWorker : MarshalByRefObject, IAppWorker
    {
        public AppServer Server { get; private set; }

        public AppWorker()
        {
            Server = new AppServer();
        }

        public void Start()
        {
            Server.Start();
        }

        public void Stop()
        {
            Server.Stop();
        }

        public void DownloadUpdateData()
        {
            throw new NotImplementedException();
        }
    }
}
