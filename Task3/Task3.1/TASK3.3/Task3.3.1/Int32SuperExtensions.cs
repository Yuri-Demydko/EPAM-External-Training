using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Task3._3._1
{
    public static class Int32SuperExtensions
        {
            public static void YApplyFunction(this int[] array, Func<int,int> function)
            {
                for (var i = 0; i < array.Length; i++)
                {
                    array[i] = function(array[i]);
                }
            }
            

            public static int YSum(this int[] array) => array.Sum(x => x);

            public static double YAverage(this int[] array) => array.Average(x => x);
            public static int YMostCommon(this int[] array)
            {
                var mc = array.Max(i => array.Count(a => a == i));
                return array
                    .First(e => array.Count(i => i == e) == mc);
            }
        }
}