using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class BaseEntry: MarshalByRefObject
    {
        public string DomainName { get; protected set; }
        public string PluginVersion { get; set; }
    }
}
