using System;
using System.Linq;

namespace Task1._2._1
{
    class Program
    {
        //Returns average word length precisely (without any rounding)
        private static double GetAverageWordLengthOfString(string str)
        {
            var splitStr = str.Split(' ');
            //Decided to keep using linq, but that aggregate function seems more efficient
            var res = splitStr.Aggregate<string, double>(0, (current, s) => 
                current + s.Count(char.IsLetterOrDigit))/splitStr.Length;
            return res;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Type a string to calculate an average word length in it:");
            Console.WriteLine($"Average word length: {GetAverageWordLengthOfString(Console.ReadLine())}");
        }
    }
}
