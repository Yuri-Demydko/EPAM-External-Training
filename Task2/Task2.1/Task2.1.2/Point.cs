using System;
using System.Collections.Generic;
using System.Reflection;

namespace YGeometry
{
    public struct Point
    {
        private readonly  double _x;
        private readonly  double _y;

        public Point(double x, double y)
        {
            _y = y;
            _x = x;
        }

       

        public double DistanceTo(Point p2) => 
            Math.Sqrt(Math.Pow((p2.X - this.X), 2) + Math.Pow((p2.Y - this.Y), 2));
        
        public static double DistanceBetween(Point p1, Point p2) => 
            Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));

        public double X => _x;
        public double Y => _y;
    }
}