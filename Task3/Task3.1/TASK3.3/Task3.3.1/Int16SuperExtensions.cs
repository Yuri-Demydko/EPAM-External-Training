using System;
using System.Linq;

namespace Task3._3._1
{
    public static class Int16SuperExtensions
    {
        public static void YApplyFunction(this short[] array, Func<short,short> function)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = function(array[i]);
            }
        }

        public static int YSum(this short[] array) => array.Sum(x => x);

        public static double YAverage(this short[] array) => array.Average(x => x);
        public static short YMostCommon(this short[] array)
        {
            var mc = array.Max(i => array.Count(a => a == i));
            return array
                .First(e => array.Count(i => i == e) == mc);
        }
    }
}