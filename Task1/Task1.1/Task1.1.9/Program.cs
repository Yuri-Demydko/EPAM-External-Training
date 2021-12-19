using System;
using System.Linq;

namespace Task1._1._9
{
    class Program
    {
        private static int[] GetRandomArray(int len) =>
            Enumerable
                .Repeat(0, len)
                .Select(i => new Random().Next(-100,100))
                .ToArray();

        private static int GetSumOfNonNegativesInArray(int[] array)
            => array.Where(i => i >= 0).Sum();
        
        static void Main(string[] args)
        {
            var arr = GetRandomArray(10);
            Console.WriteLine(GetSumOfNonNegativesInArray(arr));
        }
    }
}
