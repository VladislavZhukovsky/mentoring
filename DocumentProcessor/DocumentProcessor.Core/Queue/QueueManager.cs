﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using DocumentProcessor.Core;

namespace DocumentProcessor.Core.Queue
{
    public class QueueManager: IDisposable
    {
        private static string QUEUE_NAME = @".\Private$\DocumentProcessorQueue";

        private MessageQueue queue;

        public QueueManager()
        {
            InitializeQueue();
        }

        public void SendFiles(IEnumerable<string> files)
        {
            queue.Send(new QueueMessage() { Files = files.ToList() });
        }

        public IEnumerable<string> ReceiveFiles()
        {
            if (queue.GetMessageEnumerator2().MoveNext())
            {
                var message = queue.Peek(TimeSpan.FromSeconds(10));
                var queueMessage = (QueueMessage)message.Body;
                queue.Receive();
                return queueMessage.Files;
            }
            return null;
        }

        public void ReceiveById(string id)
        {
            queue.ReceiveById(id);
        }

        private void InitializeQueue()
        {
            if (MessageQueue.Exists(QUEUE_NAME))
            {
                queue = new MessageQueue(QUEUE_NAME);
            }
            else
            {
                queue = MessageQueue.Create(QUEUE_NAME);
            }
            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(QueueMessage) });
        }

        public void Dispose()
        {
            if (queue != null)
                queue.Close();
        }
    }
}
