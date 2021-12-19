using System;

namespace Task1._1._2
{
    class Program
    {
        static void Main(string[] args)
        {
            var stages = Convert.ToInt32(Console.ReadLine());
            for(var i=0;i<stages;i++)
            {
                for (var j = 0; j <= i; j++)
                    Console.Write("*");
                Console.WriteLine();
            }
            
        }
    }
}
