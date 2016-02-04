using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProcessor.Core.Queue
{
    [Serializable]
    public class QueueMessage
    {
        public List<string> Files { get; set; }
        public int Try { get; set; }

        public QueueMessage(IEnumerable<string> files)
        {
            Files = files.ToList();
            Try = 0;
        }

        public QueueMessage(IEnumerable<string> files, int @try)
        {
            Files = files.ToList();
            Try = @try;
        }
    }
}
 