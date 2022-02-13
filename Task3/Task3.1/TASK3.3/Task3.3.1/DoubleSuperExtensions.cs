using System;
using System.Linq;

namespace Task3._3._1
{
    public static class DoubleSuperExtensions
    {
        public static void YApplyFunction(this double[] array, Func<double,double> function)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = function(array[i]);
            }
        }

        public static double YSum(this double[] array) => array.Sum(x => x);

        public static double YAverage(this double[] array) => array.Average(x => x);
        public static double YMostCommon(this double[] array)
        {
            var mc = array.Max(i => array.Count(a => a.Equals(i)));
            return array
                .First(e => array.Count(i => i.Equals(e)) == mc);
        }
    }
}