using System;
using System.Text;

namespace Task1._1._4
{
    class Program
    {
        public static string GenerateTree(int totalStages)
        {
            var sb = new StringBuilder();
            for(int stages=0;stages<=totalStages+1;stages++)
            {
                for (var i = 0; i < stages; i++)
                {
                    for (var j = 0; j < totalStages-i ; j++) sb.Append(" ");

                    for (var m = 0; m < i+1 ; m++) sb.Append("*");

                    for (var m = 0; m < i; m++) sb.Append("*");

                    sb.Append(Environment.NewLine);
                }
            }

            return sb.ToString();
        }
        static void Main(string[] args)
        {
            Console.Write("Enter total number of stages of your tree: ");
            Console.WriteLine($"Here is your tree:{Environment.NewLine}" +
                              $"{GenerateTree(Convert.ToInt32(Console.ReadLine()))}");

        }
    }
}
