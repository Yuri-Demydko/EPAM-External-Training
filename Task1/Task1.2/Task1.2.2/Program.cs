using System;
using System.Linq;

namespace Task1._2._2
{
    class Program
    {
        private static string DoubleFirstSymbols(string originalString, string symbolsToDouble)
        {
          return originalString
                .Select(c => symbolsToDouble.Contains(c) ? $"{c}{c}":$"{c}")
                .Aggregate((s,cur)=>s + cur);
        }
        static void Main(string[] args)
        {
            Console.WriteLine(DoubleFirstSymbols(Console.ReadLine(),Console.ReadLine()));
        }
    }
}
