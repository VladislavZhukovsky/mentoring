using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginManager
{
    public class PluginLoadResult
    {
        public AppDomain Domain { get; private set; }
        public object Entry { get; private set; }

        public PluginLoadResult(AppDomain domain, object entry)
        {
            this.Domain = domain;
            this.Entry = entry;
        }
    }
}
