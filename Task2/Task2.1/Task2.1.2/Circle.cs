using System;

namespace YGeometry
{
    public class Circle : RoundBasedShape, IAreaCalculatable
    {
        public Circle(Point center, double radius) : base(center, radius)
        {
        }

        public Circle():base()
        {
        }

        public double Area => Math.Pow(Math.PI * Radius, 2);
        public override string ToString()
        {
            return base.ToString()+$"\nArea: {Area}";
        }
    }
}