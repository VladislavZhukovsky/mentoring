using DocumentProcessor.Core;
using DocumentProcessor.Core.Processors;
using DocumentProcessor.Core.Queue;
using System;
using System.Collections.Generic;
using System.IO;
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
        private IProcessor processor;
        private string workingFolder;
        private string documentFolder;

        public DocumentProcessor(string workingFolder, string documentFolder)
        {
            this.workingFolder = workingFolder;
            this.documentFolder = documentFolder;
            Directory.CreateDirectory(workingFolder);
            Directory.CreateDirectory(documentFolder);
            workThread = new Thread(WorkProcedure);
            processor = new PdfProcessor();
            getMessageEvent = new AutoResetEvent(false);
            OnStart(null);
        }

        private void WorkProcedure()
        {
            using(var queueManager = new QueueManager())
            {
                do
                {
                    Console.WriteLine("Receive");
                    var files = queueManager.ReceiveFiles();
                    if (files != null)
                    {
                        Console.WriteLine("Process");

                        processor.Process(files, workingFolder, documentFolder);
                    }
                }
                while (!getMessageEvent.WaitOne(TimeSpan.FromSeconds(7)));
            }
        }


        protected /*override*/ void OnStart(string[] args)
        {
            getMessageEvent.Reset();
            workThread.Start();
        }

        //protected override void OnStop()
        //{
        //    base.OnStop();
        //}
    }
}
