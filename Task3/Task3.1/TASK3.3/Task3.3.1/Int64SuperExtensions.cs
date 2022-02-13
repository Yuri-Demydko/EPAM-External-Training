using System;
using System.Linq;

namespace Task3._3._1
{
    public static class Int64SuperExtensions
    {
        public static void YApplyFunction(this long[] array, Func<long,long> function)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = function(array[i]);
            }
        }

        public static long YSum(this long[] array) => array.Sum(x => x);

        public static double YAverage(this long[] array) => array.Average(x => x);
        public static long YMostCommon(this long[] array)
        {
            var mc = array.Max(i => array.Count(a => a == i));
            return array
                .First(e => array.Count(i => i == e) == mc);
        }
    }
}