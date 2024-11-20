using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int? Age = null;

            string PersonAge = Age?.ToString() ?? "Hi";

            Console.WriteLine("Age is " + PersonAge);
        }
    }
}
