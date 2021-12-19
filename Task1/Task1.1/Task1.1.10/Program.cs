using System;

namespace Task1._1._10
{
    class Program
    {
        private static int[,] GetRandom2DArray(int l1,int l2)
        {
            var rand = new Random();
            var res = new int[l1,l2];
            
            for (var i = 0; i < l1; i++)
                for (var j = 0; j < l2; j++)
                    res[i,j] = rand.Next(100);
            
            return res;
        }

        private static int GetSumOfEvenPositionElements(int[,] array)
        {
            var res = 0;
            for (var i = 0; i < array.GetLength(0); i++)
                for (var j = 0; j < array.GetLength(1); j++)
                    res += (i + j) % 2 == 0 ? array[i, j] : 0;
            return res;
        }
        static void Main(string[] args)
        {
            var arr = GetRandom2DArray(5, 5);
            Console.WriteLine($"Sum of elements of even positions: {GetSumOfEvenPositionElements(arr)}");
        }
    }
}
