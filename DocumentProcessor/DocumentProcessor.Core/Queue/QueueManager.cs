using System;
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
            var message = queue.Receive();
            var queueMessage = (QueueMessage)message.Body;
            return queueMessage.Files;
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
            queue.Formatter = new XmlMessageFormatter();
        }

        public void Dispose()
        {
            if (queue != null)
                queue.Close();
        }
    }
}
