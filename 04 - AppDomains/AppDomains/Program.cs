using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDomains
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void OtherDomainThreadCheck()
        {
            MessageBox.Show(AppDomain.CurrentDomain.FriendlyName + "Thread " + Thread.CurrentThread.ManagedThreadId);

            var domainSetup = new AppDomainSetup()
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory
            };

            var domain = AppDomain.CreateDomain("domainForOtherApp", new System.Security.Policy.Evidence(), domainSetup);
            domain.ExecuteAssemblyByName("OtherApp");
            AppDomain.Unload(domain);
        }
    }
}
