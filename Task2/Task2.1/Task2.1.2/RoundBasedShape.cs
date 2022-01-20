using System;
using System.Linq;

namespace YGeometry
{
    public abstract class RoundBasedShape : Shape
    {
        private readonly double _radius;
        public double Radius => _radius;

        public Point Center => points.First();
        protected override int SuitablePointsNumber => 1;
        public override double TotalLength => 2 * Math.PI * Radius;

        public override string ToString()
        {
            return (base.ToString()+$"\nCenter: {Center.X} , {Center.Y}\nRadius: {Radius}").Replace("@@","Circumference length");
        }

        protected RoundBasedShape(Point center, double radius):base(center)
        {
            this._radius = radius;
        }

        protected RoundBasedShape():base()
        {
            Console.Write($"Enter radius: ");
            this._radius = Convert.ToDouble(Console.ReadLine());
        }
    }
}