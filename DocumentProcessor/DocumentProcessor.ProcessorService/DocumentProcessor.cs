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
        private const string FAILED_FOLDER_NAME = "Failed";

        private Thread workThread;
        private AutoResetEvent getMessageEvent;
        private QueueManager queueManager;
        private IProcessor processor;
        private string workingFolder;
        private string documentFolder;
        private string failedFolder;

        public DocumentProcessor(string workingFolder, string documentFolder)
        {
            this.workingFolder = workingFolder;
            this.documentFolder = documentFolder;
            this.failedFolder = Path.Combine(workingFolder, failedFolder);
            Directory.CreateDirectory(workingFolder);
            Directory.CreateDirectory(documentFolder);
            Directory.CreateDirectory(failedFolder);
            workThread = new Thread(WorkProcedure);
            processor = new PdfProcessor();
            getMessageEvent = new AutoResetEvent(false);
            OnStart(null);
        }

        private void WorkProcedure()
        {
            using(queueManager = new QueueManager())
            {
                do
                {
                    Console.WriteLine("Receive");
                    var message = queueManager.ReceiveMessage();
                    var task = new Task(() => ProcessFiles(message.Files, message.Try));
                    task.Start();
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

        private void ProcessFiles(IEnumerable<string> files, int @try)
        {
            if (files != null)
            {
                Console.WriteLine("Process");
                var result = processor.Process(files, workingFolder, documentFolder);
                if (result.Result == ProcessingResult.Failed)
                {
                    if (@try < 2)
                    {
                        //logger
                        queueManager.SendMessage(files, @try + 1);
                    }
                    else
                    {
                        //logger
                        var failedPath = MoveToFailed(files);
                    }
                }
            }
        }

        private string MoveToFailed(IEnumerable<string> files)
        {
            var chainFolderPath = Path.Combine(workingFolder, failedFolder, Guid.NewGuid().ToString());
            foreach(var file in files)
            {
                var filepath = Path.Combine(workingFolder, file);
                if (File.Exists(filepath))
                    File.Move(filepath, chainFolderPath);
            }
            return chainFolderPath;
        }
    }
}
