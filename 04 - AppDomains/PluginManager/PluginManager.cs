using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginManager
{
    public class PluginManager
    {
        public PluginLoadResult LoadPlugin(string path, string entryPointName)
        {
            var assemblyName = System.IO.Path.GetFileNameWithoutExtension(path);

            var domainSetup = new AppDomainSetup()
            {
                ApplicationBase = System.IO.Path.GetDirectoryName(path)
            };

            var domain = AppDomain.CreateDomain(NewDomainName(), new System.Security.Policy.Evidence(), domainSetup);
            domain.Load(assemblyName);
            var entry = domain.CreateInstanceAndUnwrap(assemblyName, assemblyName + "." + entryPointName);
            return new PluginLoadResult(domain, entry);
        }

        public void UnloadPlugin(AppDomain domain)
        {
            try
            {
                AppDomain.Unload(domain);
            }
            catch(CannotUnloadAppDomainException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string NewDomainName()
        {
            var result = "AppDomain_";
            result += new Random(Guid.NewGuid().GetHashCode()).Next(1000);
            return result;
        }
    }
}
