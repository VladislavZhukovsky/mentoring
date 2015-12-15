using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginV1
{
    public class Entry: MarshalByRefObject
    {
        public string DomainName { get; private set; }

        public string PluginVersion { get; set; }

        public Entry()
        {
            DomainName = AppDomain.CurrentDomain.FriendlyName;
            PluginVersion = "v1";
        }
    }
}
