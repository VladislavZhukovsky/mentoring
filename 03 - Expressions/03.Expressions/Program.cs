using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _03.Expressions;
using System.Linq.Expressions;
using System.Diagnostics;

namespace _03.Expressions
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> func = x => x + 3;
            Expression<Func<int, int>> expr = x => x + 3;
            Debug.WriteLine(func);
            var visitor = new TraceExpressionVisitor();
            visitor.Visit(expr);

            Expression<Func<object>> expr1 = () => new { Name = "Vlad" };

            Console.ReadKey();
        }
    }
}
