using System;
using System.Linq;

namespace Task1._1._5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
            Enumerable
                .Range(1, 999)
                .Where(i => (i % 3 == 0) || (i % 5 == 0))
                .Sum()
            );

        }
    }
}
