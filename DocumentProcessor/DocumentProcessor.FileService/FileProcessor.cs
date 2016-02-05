﻿using DocumentProcessor.Core.Queue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentProcessor.FileService
{
    class FileProcessor
    {
        internal const string ServiceName = "FileProcessorService";
        private const int MAX_CHAIN_CAPACITY = 3;

        private Thread workThread;
        private ManualResetEvent stopWorkEvent = new ManualResetEvent(false);
        private AutoResetEvent sourceDirectoryChangedEvent = new AutoResetEvent(false);
        private string sourcePath;
        private string destinationPath;

        private FileSystemWatcher fileWatcher;

        public FileProcessor(string inRootPath, string outRootPath)
        {
            //this.CanStop = true;
            //this.ServiceName = FileProcessor.SericeName;
            //this.AutoLog = false;

            sourcePath = inRootPath;
            destinationPath = outRootPath;
            workThread = new Thread(WorkProcedure);
            Directory.CreateDirectory(inRootPath);
            Directory.CreateDirectory(outRootPath);
            fileWatcher = new FileSystemWatcher(sourcePath);
            fileWatcher.EnableRaisingEvents = false;

            fileWatcher.Changed += SourceDirectoryChanged;
            fileWatcher.Created += SourceDirectoryChanged;
            fileWatcher.Renamed += SourceDirectoryChanged;
            OnStart(null);
        }

        private void SourceDirectoryChanged(object sender, FileSystemEventArgs e)
        {
            sourceDirectoryChangedEvent.Set();
        }

        protected /*override */void OnStart(string[] args)
        {
            stopWorkEvent.Reset();
            sourceDirectoryChangedEvent.Reset();
            fileWatcher.EnableRaisingEvents = true;
            workThread.Start();
        }

        //protected override void OnStop()
        //{
        //    fileWatcher.EnableRaisingEvents = false;
        //    stopWorkEvent.Set();
        //    workThread.Join();
        //}

        protected void WorkProcedure(object obj)
        {
            var movedFiles = new List<string>();
            using (var queueManager = new QueueManager())
            {
                do
                {
                    if (stopWorkEvent.WaitOne(TimeSpan.Zero))
                        return;
                    Console.WriteLine("Scan");
                    var files = Directory.EnumerateFiles(sourcePath);
                    if (files.Count() >= 3)
                    {
                        foreach (var fileInfo in files)
                        {

                            FileStream file;

                            if (TryOpen(fileInfo, out file, FileMode.Open, FileAccess.ReadWrite, FileShare.None, 3))
                            {
                                file.Close();

                                try
                                {
                                    Console.WriteLine("Move");
                                    File.Move(fileInfo, Path.Combine(destinationPath, Path.GetFileName(fileInfo)));
                                    movedFiles.Add(Path.GetFileName(fileInfo));
                                }
                                catch (IOException ex)
                                {
                                    //log file not sent
                                }
                            }
                        }
                        queueManager.SendMessage(movedFiles);
                        movedFiles.Clear();
                    }
                }
                while (
                    WaitHandle.WaitAny(new WaitHandle[] { stopWorkEvent, sourceDirectoryChangedEvent }, TimeSpan.FromMilliseconds(10000)) != 0
                );
            }
        }

        private bool TryOpen(string fileName, out FileStream file, FileMode fileMode, FileAccess fileAccess, FileShare fileShare, int count)
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    file = new FileStream(fileName, fileMode, fileAccess, fileShare);

                    return true;
                }
                catch (IOException)
                {
                    Thread.Sleep(5000);
                }
            }

            file = null;
            return false;
        }
    }
}
