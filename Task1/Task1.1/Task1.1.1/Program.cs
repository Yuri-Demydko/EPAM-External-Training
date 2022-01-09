using System;
using System.Data;

namespace Task1._1._1
{
    class Program
    {
        public static int CalculateArea(int len, int wid) => len * wid;
        public static int ReadPositiveInt(string str) => Math.Abs(Convert.ToInt32(str));
        static void Main(string[] args)
        {
            Console.Write("Enter length: ");
            var len = ReadPositiveInt(Console.ReadLine());
            Console.Write("Enter width: ");
            var wid = ReadPositiveInt(Console.ReadLine());
            Console.WriteLine($"Area of that rectangle: {CalculateArea(len,wid)}");
        }
    }
}
