using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace YGeometry
{
    public class Triangle : Shape,IAreaCalculatable
    {
        protected override int SuitablePointsNumber => 3;

        public override double TotalLength =>
            points.ElementAt(0)
                .DistanceTo(points.ElementAt(1))
            + points.ElementAt(1)
                .DistanceTo(points.ElementAt(2))
            + points.ElementAt(2)
                .DistanceTo(points.ElementAt(0));

        public double Area => Math.Sqrt(
            (TotalLength / 2 - points.ElementAt(0)
                .DistanceTo(points.ElementAt(1))) *
            (TotalLength / 2 - points.ElementAt(1)
                .DistanceTo(points.ElementAt(2))) *
            (TotalLength / 2 - points.ElementAt(2)
                .DistanceTo(points.ElementAt(0))) *
            TotalLength / 2);

        public Triangle(Point p1, Point p2, Point p3):base(p1,p2,p3)
        {
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var res= base.Validate(validationContext).ToList();
            var sideA = points.ElementAt(0).DistanceTo(points.ElementAt(1));
            var sideB = points.ElementAt(1).DistanceTo(points.ElementAt(2));
            var sideC = points.ElementAt(0).DistanceTo(points.ElementAt(2));
            if(sideC>sideA+sideB||
               sideB>sideA+sideC ||
               sideA>sideB+sideC)
                res.Add(new ValidationResult("Wrong sides length!"));
            return res;
        }

        public override string ToString()
        {
            return base.ToString().Replace("@@","Perimeter")+$"\nArea: {Area}";
        }
    }
}