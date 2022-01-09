using System;
using System.Linq;

namespace Task1._1._5
{
    class Program
    {
        public static int GetSumOfNumbers(int minimum, int maximum, Func<int, bool> predicate) =>
            Enumerable
                .Range(minimum, maximum-1)
                .Where(i => predicate(i))
                .Sum();

        static void Main(string[] args)
        {
            Console.WriteLine($"This is sum of all numbers between 1 and 1000 which can be divided by 3 or 5:{Environment.NewLine}"+
            GetSumOfNumbers(1,1000,i=>(i % 3 == 0) || (i % 5 == 0))
            );

        }
    }
}
