using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            var setup1 = new AppDomainSetup() { ApplicationBase = AppDomain.CurrentDomain.BaseDirectory };
            var domain1 = AppDomain.CreateDomain("AD1", new System.Security.Policy.Evidence(), setup1);
            domain1.Load("PluginV1");


            var setup2 = new AppDomainSetup() { ApplicationBase = AppDomain.CurrentDomain.BaseDirectory };
            var domain2 = AppDomain.CreateDomain("AD2", new System.Security.Policy.Evidence(), setup2);
            
        }
    }
}
