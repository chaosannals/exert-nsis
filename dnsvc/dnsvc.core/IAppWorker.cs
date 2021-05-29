using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnSvc
{
    public interface IAppWorker
    {
        void Start();
        void Stop();
        void DownloadUpdateData();
    }
}
