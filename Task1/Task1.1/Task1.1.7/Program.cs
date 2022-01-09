using System;
using System.Linq;
using System.Text;

namespace Task1._1._7
{
    class Program
    {
        private static int[] GetRandomArray(int length)
        {
            var rand = new Random();
            var res = new int[length];
            for (var i = 0; i < length; i++)
            {
                res[i] = rand.Next(100);
            }

            return res;
        }

        private static string StringifyArray(int[] array,char splitter)
        {
            var sb = new StringBuilder();
            foreach (var i in array)
            {
                sb.Append($"{i}{splitter}");
            }

            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        private static void SortArray(ref int[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                for (var j = 0; j < array.Length-1; j++)
                    if (array[j] > array[j + 1])
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
            }
        }

        private static (int min, int max) GetMinMaxValues(int[] array)
        {
            var min = array[0];
            var max = array[0];
            foreach (var i in array)
            {
                if (min > i) min = i;
                if (max < i) max = i;
            }

            return (min, max);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to generate, sort random array and find minimum and maximum number of it!");
            Console.ReadKey();
            var arr= GetRandomArray(10); 
            Console.WriteLine("Original array:");
            Console.WriteLine(StringifyArray(arr,' '));
           
           SortArray(ref arr);
           Console.WriteLine("Sorted array:");
           Console.WriteLine(StringifyArray(arr,' '));
           var (min, max) = GetMinMaxValues(arr);
           Console.WriteLine($"Minimum: {min}\nMaximum: {max}");
        }
    }
}
