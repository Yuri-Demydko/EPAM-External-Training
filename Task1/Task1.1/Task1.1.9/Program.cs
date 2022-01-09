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
            Console.Write("Enter length of random array which will be generated: ");
            var arr = GetRandomArray(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine($"Here is sum of a non-negative numbers in that array: {GetSumOfNonNegativesInArray(arr)}");
        }
    }
}
