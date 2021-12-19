using System;

namespace Task1._1._4
{
    class Program
    {
        static void Main(string[] args)
        {
            var totalStages = Convert.ToInt32(Console.ReadLine());
            for(int stages=0;stages<=totalStages+1;stages++)
            {
                for (var i = 0; i < stages; i++)
                {
                    for (var j = 0; j < totalStages-i ; j++) Console.Write(" ");

                    for (var m = 0; m < i+1 ; m++) Console.Write("*");

                    for (var m = 0; m < i; m++) Console.Write("*");

                    Console.WriteLine();
                }
            }
        }
    }
}
