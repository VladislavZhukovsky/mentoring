using DocumentProcessor.Core.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentProcessor.ProcessorService
{
    public class DocumentProcessor//: ServiceBase
    {
        private Thread workThread;
        private AutoResetEvent getMessageEvent;

        public DocumentProcessor()
        {
            workThread = new Thread(WorkProcedure);
        }

        private void WorkProcedure()
        {
            do
            {
                using(var queueManager = new QueueManager())
                {
                    var files = queueManager.ReceiveFiles();
                }
            }
            while (!getMessageEvent.WaitOne(TimeSpan.FromSeconds(7)));
        }


        protected /*override*/ void OnStart(string[] args)
        {
            workThread.Start();
        }

        //protected override void OnStop()
        //{
        //    base.OnStop();
        //}
    }
}
