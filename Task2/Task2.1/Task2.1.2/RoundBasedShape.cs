using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var res= base.Validate(validationContext).ToList();
            if (_radius <= 0) res.Add(new ValidationResult("Radius must be positive number!"));
            return res;
        }
    }
}