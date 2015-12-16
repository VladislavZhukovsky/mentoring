using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginV2
{
    public class Entry
    {
        public string DomainName { get; private set; }

        public string PluginVersion { get; set; }

        public Entry()
        {
            DomainName = AppDomain.CurrentDomain.FriendlyName;
            PluginVersion = "v2";
        }
    }
}
