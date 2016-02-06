using DocumentProcessor.Core;
using DocumentProcessor.Core.Processors;
using DocumentProcessor.Core.Queue;
using NLog;
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
        private string workingFolder;
        private string documentFolder;
        private string failedFolder;
        private Logger logger;
        private List<Task> tasks;

        public DocumentProcessor(string workingFolder, string documentFolder)
        {
            this.workingFolder = workingFolder;
            this.documentFolder = documentFolder;
            this.failedFolder = Path.Combine(workingFolder, FAILED_FOLDER_NAME);
            Directory.CreateDirectory(workingFolder);
            Directory.CreateDirectory(documentFolder);
            Directory.CreateDirectory(failedFolder);
            logger = LogManager.GetCurrentClassLogger();
            tasks = new List<Task>();
            workThread = new Thread(WorkProcedure);
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
                        logger.Info("Waiting for all processing documents");
                        Task.WaitAll(tasks.ToArray(), TimeSpan.FromMinutes(1).Milliseconds);
                        logger.Info("All documents were processed");
                        return;
                    }
                    logger.Info("Receiving message...");
                    var message = queueManager.ReceiveMessage();
                    if (message != null)
                    {
                        logger.Info("INFO | Processing message with id {0}", message.Id);
                        var task = new Task(() => ProcessMessage(message));
                        tasks.Add(task);
                        task.Start();
                    }
                    else
                    {
                        logger.Info("Queue is empty");
                    }
                    tasks.RemoveAll(x => x.IsCompleted);
                }
                while (!stopWorkEvent.WaitOne(TimeSpan.FromSeconds(7)));
            }
        }

        protected /*override*/ void OnStart(string[] args)
        {
            logger.Info("=====Starting service...");
            stopWorkEvent.Reset();
            workThread.Start();
        }

        //protected override void OnStop()
        //{
        //    logger.Info("=====Stopping service...");
        //    stopWorkEvent.Set();
        //    workThread.Join();
        //}

        private void ProcessMessage(QueueMessage message)
        {
            var logBuilder = new StringBuilder();
            logBuilder.AppendLine(string.Format("INFO | Start processing message with id {0}", message.Id));
            if (message.Files != null)
            {
                IProcessor processor = new PdfProcessor();
                var result = processor.Process(message.Files, workingFolder, documentFolder);
                logBuilder.AppendLine("INFO | Document processor log");
                logBuilder.AppendLine(result.Log);
                if (result.Result == ProcessingResult.Failed)
                {
                    logBuilder.AppendLine(string.Format("WARN | Message {0} processing failed", message.Id));
                    if (message.Attempt < 3)
                    {
                        message.Attempt++;
                        logBuilder.AppendLine(string.Format("INFO | Return message {0} to queue; attempt: {1}", message.Id, message.Attempt));
                        queueManager.SendMessage(message);
                    }
                    else
                    {
                        logBuilder.AppendLine(string.Format("ERROR | Message {0} failed, moving to {1} folder", message.Id, FAILED_FOLDER_NAME));
                        var failedPath = MoveToFailed(message.Files);
                        logBuilder.AppendLine(string.Format("INFO | Message {0} failed, moved to {1} folder", message.Id, failedPath));
                    }
                }
                if (result.Result == ProcessingResult.Success)
                {
                    logBuilder.AppendLine(string.Format("INFO | Message {0} processed successfully", message.Id));
                }
            }
            else
            {
                logBuilder.AppendLine(string.Format("INFO | No files in message {0}", message.Id));
            }
            logBuilder.AppendLine(string.Format("INFO | Processing message {0} finished", message.Id));
            logger.Info(logBuilder.ToString());
        }

        private string MoveToFailed(IEnumerable<string> files)
        {
            var chainFolderName = Guid.NewGuid().ToString();
            var chainFolderPath = Path.Combine(workingFolder, failedFolder, chainFolderName);
            Directory.CreateDirectory(chainFolderPath);
            foreach(var file in files)
            {
                var filepath = Path.Combine(workingFolder, file);
                if (File.Exists(filepath))
                    File.Move(filepath, Path.Combine(chainFolderPath, file));
            }
            return chainFolderPath;
        }
    }
}
