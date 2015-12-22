using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PowerManagement;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var adapter = new PowerManagementAdapter();
                //var result = adapter.GetLastSleepTime();
                //var result = adapter.GetLastWakeTime();
                //var result = adapter.GetSystemBatteryState();
                var result = adapter.GetSystemPowerInformation();
                //adapter.ReserveHibernationFile();
                //adapter.RemoveHibernationFile();
                Console.WriteLine(result.ToString());
                Console.WriteLine("done");
                Console.WriteLine("press any key...");
            }
            catch (PowerManagement.Exceptions.PowerManagementException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
