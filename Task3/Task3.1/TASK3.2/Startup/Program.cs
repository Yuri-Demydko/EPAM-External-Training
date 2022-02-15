using System;
using System.Data.Common;
using DynamicArray;

namespace Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            var basis = new[] {"1","2","3"};
            var dyn = new DynamicArray<string>(basis);
            //Console.WriteLine(dyn.Length);
            dyn[10] = "olol";
            
            Console.WriteLine("Len: "+dyn.Length);
            foreach (var d in dyn)
            {
                Console.Write(d + " ");
            }

            Console.WriteLine(dyn[102]);
        }
    }
}