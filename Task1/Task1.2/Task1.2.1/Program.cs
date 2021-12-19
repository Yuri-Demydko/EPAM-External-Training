using System;
using System.Linq;

namespace Task1._2._1
{
    class Program
    {
        //Returns average word length precisely (without any rounding)
        private static double GetAverageWordLengthOfString(string str) =>
            str.Split(' ')
                .Select(s => s.Where(char.IsLetterOrDigit).ToArray())
                .Average(s => s.Length);

        static void Main(string[] args)
        {
            Console.WriteLine(GetAverageWordLengthOfString(Console.ReadLine()));
        }
    }
}
