using System;
using System.Data;

namespace Task1._1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var len = Convert.ToInt32(Console.ReadLine());
            var wid = Convert.ToInt32(Console.ReadLine());
            if (len <= 0 || wid <= 0) throw new ArithmeticException("Length and Width must be greater than 0!");
            Console.WriteLine($"Area of that rectangle: {len*wid}");
        }
    }
}
