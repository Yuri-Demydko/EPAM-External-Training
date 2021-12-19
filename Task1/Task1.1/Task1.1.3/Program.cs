using System;

namespace Task1._1._3
{
    class Program
    {
        static void Main(string[] args)
        {
            var stages = Convert.ToInt32(Console.ReadLine());
            for (var i = 0; i < stages; i++)
            {
                for (var j = 0; j <stages-i ; j++)
                    Console.Write(" ");
                
                for (var m = 0; m < i+1; m++)
                    Console.Write("*");
                
                for (var m = 0; m < i; m++)
                    Console.Write("*");
                
                Console.WriteLine();
            }
        }
    }
}
