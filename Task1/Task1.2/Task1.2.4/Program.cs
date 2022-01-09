using System;
using System.Linq;
using System.Text;

namespace Task1._2._4
{
    class Program
    {
        private static string FixSentenceFirstSymbols(string str)
        {
            //Thought it could be better if "?!" and "..." characters will be delimiters too
            var res = new StringBuilder();
            var splitters = new [] {"...", ".", "!", "?", "?!"};

            var split= str
               .Split(' ', StringSplitOptions.TrimEntries);

            res.Append(split[0][0].ToString().ToUpper() + split[0][1..]).Append(' ');
            for (var i = 1; i < split.Length; i++)
            {
                if (splitters.Any(s => split[i - 1].Contains(s)))
                    res.Append(split[i][0].ToString().ToUpper() + split[i][1..]).Append(' ');
                else res.Append(split[i]).Append(' ');
            }

            return res.ToString();
        }
        static void Main()
        {
            Console.WriteLine("Enter some string to UPPERCASE first letters of it's sentences:");
            var str = Console.ReadLine();
            Console.WriteLine(FixSentenceFirstSymbols(str));
        }
    }
}
