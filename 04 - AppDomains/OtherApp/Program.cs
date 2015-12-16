using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageBox.Show(AppDomain.CurrentDomain.FriendlyName + "Thread " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
