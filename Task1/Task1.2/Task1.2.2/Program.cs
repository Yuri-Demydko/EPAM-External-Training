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
            Console.WriteLine("Enter any string:");
            var s1 = Console.ReadLine();
            Console.WriteLine("Enter string, containing symbols which will be doubled in first string:");
            var s2 = Console.ReadLine();
            Console.WriteLine($"Done! Your result:{Environment.NewLine}{DoubleFirstSymbols(s1,s2)}");
        }
    }
}
