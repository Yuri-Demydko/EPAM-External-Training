using System;

namespace Task1._1._8
{
    class Program
    {
        private static int[,,] GetRandom3DArray(int l1, int l2, int l3)
        {
            var rand = new Random();
            var result = new int[l1, l2, l3];
            for (var i = 0; i < l1; i++)
                for (var j = 0; j < l2; j++)
                    for (var h = 0; h < l2; h++)
                        result[i, j, h] = rand.Next(-100,100);
            return result;
        }
        private static void Make3DArrayNoPositive(ref int[,,] array)
        {
            for (var i = 0; i < array.GetLength(0); i++)
                for (var j = 0; j < array.GetLength(1); j++)
                    for (var h = 0; h < array.GetLength(2); h++)
                        array[i, j, h] = array[i, j, h] > 0 ? 0 : array[i, j, h];
        }
        static void Main(string[] args)
        {
            var arr3d = GetRandom3DArray(5, 5, 5);
            Make3DArrayNoPositive(ref arr3d);
            
        }
    }
}
