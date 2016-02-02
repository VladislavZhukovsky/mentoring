using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProcessor.Core
{
    public interface IProcessor
    {
        void Process(IEnumerable<string> files, string destinationFolder);
    }
}
