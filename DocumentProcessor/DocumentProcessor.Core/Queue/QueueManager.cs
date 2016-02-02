using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProcessor.Core.Queue
{
    public class QueueManager: IDisposable
    {
        private MessageQueue queue;
        private IProcessor processor;

        public QueueManager(string queueName, IProcessor processor)
        {
            InitializeQueue(queueName);
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

        private void InitializeQueue(string queueName)
        {
            if (MessageQueue.Exists(queueName))
            {
                queue = new MessageQueue(queueName);
            }
            else
            {
                queue = MessageQueue.Create(queueName);
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
