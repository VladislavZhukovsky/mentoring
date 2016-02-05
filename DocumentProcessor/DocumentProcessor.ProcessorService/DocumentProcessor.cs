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
        private ManualResetEvent stopWorkEvent = new ManualResetEvent(false);
        private QueueManager queueManager;
        private IProcessor processor;
        private string workingFolder;
        private string documentFolder;
        private string failedFolder;
        private List<Task> tasks;

        public DocumentProcessor(string workingFolder, string documentFolder)
        {
            this.workingFolder = workingFolder;
            this.documentFolder = documentFolder;
            this.failedFolder = Path.Combine(workingFolder, FAILED_FOLDER_NAME);
            Directory.CreateDirectory(workingFolder);
            Directory.CreateDirectory(documentFolder);
            Directory.CreateDirectory(failedFolder);
            tasks = new List<Task>();
            workThread = new Thread(WorkProcedure);
            processor = new PdfProcessor();
            OnStart(null);
        }

        private void WorkProcedure()
        {
            using(queueManager = new QueueManager())
            {
                do
                {
                    if (stopWorkEvent.WaitOne(TimeSpan.Zero))
                    {
                        Task.WaitAll(tasks.ToArray(), TimeSpan.FromMinutes(1).Milliseconds);
                        return;
                    }
                    Console.WriteLine("Receive");
                    var message = queueManager.ReceiveMessage();
                    var task = new Task(() => ProcessFiles(message.Files, message.Try));
                    tasks.Add(task);
                    task.Start();
                    tasks.RemoveAll(x => x.IsCompleted);
                }
                while (!stopWorkEvent.WaitOne(TimeSpan.FromSeconds(7)));
            }
        }

        protected /*override*/ void OnStart(string[] args)
        {
            stopWorkEvent.Reset();
            workThread.Start();
        }

        //protected override void OnStop()
        //{
        //    stopWorkEvent.Set();
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
