using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class PluginManager
    {
        public object LoadPlugin(string path, string entryPointName)
        {
            var assemblyName = System.IO.Path.GetFileName(path);

            var domainSetup = new AppDomainSetup()
            {
                ApplicationBase = System.IO.Path.GetDirectoryName(path)
            };

            var domain = AppDomain.CreateDomain(NewDomainName(), new System.Security.Policy.Evidence(), domainSetup);
            domain.Load(assemblyName);
            var entry = domain.CreateInstanceAndUnwrap(assemblyName, assemblyName + "." + entryPointName);
            return entry;
        }

        public string NewDomainName()
        {
            var result = "AppDomain_";
            result += Guid.NewGuid().GetHashCode().ToString();
            return result;
        }
    }
}
