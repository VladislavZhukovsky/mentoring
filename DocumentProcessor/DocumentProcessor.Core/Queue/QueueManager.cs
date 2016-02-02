using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProcessor.Core.Queue
{
    public class QueueManager
    {
        private MessageQueue queue;

        public QueueManager(string queueName)
        {
            InitializeQueue(queueName);
        }

        public void SendFiles(IEnumerable<string> files)
        {
            using (queue)
            {
                queue.Send(new QueueMessage() { Files = files.ToList() });
            }
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
    }
}
