using System;
using System.Linq;

namespace Task1._2._3
{
    class Program
    {

        private static int CountLowercaseStartingWords(string str) =>
            str.Split(' ')
                .Select(s => s.Where(char.IsLetterOrDigit).ToArray())
                .Count(c => char.IsLower(c[0]));

        static void Main(string[] args)
        {
            Console.WriteLine("Enter any string to calculate words starting with lowercase characters in it:");
            var str = Console.ReadLine();
            Console.WriteLine("Count of words starting with lowercase characters: " +
                              $"{CountLowercaseStartingWords(str)}");
        }
    }
}
