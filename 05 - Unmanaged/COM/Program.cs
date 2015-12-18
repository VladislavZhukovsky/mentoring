using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskScheduler.TaskScheduler sheduler = new TaskScheduler.TaskScheduler();
            sheduler.Connect();

            var folders = sheduler.GetFolder("\\Microsoft\\Windows\\WindowsUpdate");

            foreach (TaskScheduler.IRegisteredTask task in folders.GetTasks((int)TaskScheduler._TASK_ENUM_FLAGS.TASK_ENUM_HIDDEN))
            {
                Console.WriteLine("{0} ({1}): {2} - {3}", task.Name, task.LastTaskResult, task.LastRunTime, task.NumberOfMissedRuns);
            }
            Console.ReadKey();
        }
    }
}
