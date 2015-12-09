using System;
using Sample03.E3SClient.Entities;
using Sample03.E3SClient;
using System.Configuration;
using System.Linq;

namespace Sample03
{
    public class E3SProviderTests
    {
        public static void Main(string[] args)
        {
            WithProvider();
            Console.ReadKey();
        }


        public static void WithoutProvider()
        {
            var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
            var res = client.SearchFTS<EmployeeEntity>("workstation:(EPBYMINW4226)", 0, 1);

            foreach (var emp in res)
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }

        public static void WithoutProviderNonGeneric()
        {
            var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
            var res = client.SearchFTS(typeof(EmployeeEntity), "workstation:(EPBYMINW4226)", 0, 10);

            foreach (var emp in res.OfType<EmployeeEntity>())
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }


        public static void WithProvider()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation.StartsWith("EPBYMINW")))
            //foreach (var emp in employees.Where(e => e.workstation.EndsWith("226")))
            //foreach (var emp in employees.Where(e => e.workstation.Contains("MINW4")))
            //foreach (var emp in employees.Where(e => e.workstation == "EPBYMINW4226"))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }
    }
}
