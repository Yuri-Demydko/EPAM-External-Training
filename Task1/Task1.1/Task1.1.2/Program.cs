using System;
using System.Text;

namespace Task1._1._2
{
    class Program
    {
        public static string GenerateStairs(int stages)
        {
            StringBuilder sb = new StringBuilder();
            for(var i=0;i<stages;i++)
            {
                for (var j = 0; j <= i; j++)
                    sb.Append("*");
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
        static void Main(string[] args)
        {
            Console.Write("Enter stages count of the stairs: ");
            Console.WriteLine($"Here is your stairs:{Environment.NewLine}" +
                              $"{GenerateStairs(Convert.ToInt32(Console.ReadLine()))}");
        }
    }
}
