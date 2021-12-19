using System;
using System.Linq;

namespace Task1._2._4
{
    class Program
    {
        private static string FixSentenceFirstSymbols(string str)
        {
            //Thought it could be better if "?!" and "..." characters will be delimiters too
            var res = string.Empty;
            var splitters = new [] {"...", ".", "!", "?", "?!"};

            var split= str
               .Split(' ', StringSplitOptions.TrimEntries);

            res += split[0][0].ToString().ToUpper() + split[0][1..]+" ";
            for (var i = 1; i < split.Length; i++)
            {
                if (splitters.Any(s => split[i - 1].Contains(s)))
                    res += split[i][0].ToString().ToUpper() + split[i][1..]+" ";
                else res += split[i]+" ";
            }

            return res;
        }
        static void Main()
        {
            Console.WriteLine(FixSentenceFirstSymbols(Console.ReadLine()));
        }
    }
}
