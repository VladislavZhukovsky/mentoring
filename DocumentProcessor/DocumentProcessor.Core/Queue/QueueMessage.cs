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
    }
}
 