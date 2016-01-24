using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class MyService: ServiceBase
    {
        internal MyService()
        {
            CanStop = true;
            ServiceName = "MyService";
            AutoLog = false;
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("MyService started");
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("MyService stopped");
        }
    }
}
