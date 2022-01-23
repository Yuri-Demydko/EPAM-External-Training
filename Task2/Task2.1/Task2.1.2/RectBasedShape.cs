using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace YGeometry
{
    public abstract class RectBasedShape : Shape,IAreaCalculatable
    {
        public double Area =>
            Point.DistanceBetween(points.Take(2).First(), points.Take(2).Last())
            * Point.DistanceBetween(points.TakeLast(2).First(), points.TakeLast(2).Last());

        protected override int SuitablePointsNumber => 4;

        public override double TotalLength
        {
            get
            {
                double res = 0;
                for (var i = 0; i < points.Count()-1; i++)
                    res += points.ElementAt(i).DistanceTo(points.ElementAt(i + 1));
                return res+points.Last().DistanceTo(points.First());
            }
        }
        protected RectBasedShape(Point p1, Point p2, Point p3, Point p4):base(p1,p2,p3,p4)
        {
            
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var res= base.Validate(validationContext).ToList();

            var sideA = points.ElementAt(0).DistanceTo(points.ElementAt(1));
            var sideB = points.ElementAt(1).DistanceTo(points.ElementAt(2));
            var sideC = points.ElementAt(2).DistanceTo(points.ElementAt(3));
            var sideD = points.ElementAt(3).DistanceTo(points.ElementAt(0));
            var hipo = points.ElementAt(0).DistanceTo(points.ElementAt(2));
            if(sideA!=sideC||sideB!=sideD) res.Add(new ValidationResult("Invalid sides length!"));
            if(Math.Sqrt(Math.Pow(sideA,2)+Math.Pow(sideB,2))!=hipo)
                res.Add(new ValidationResult($"Invalid angle (!=90 deg)! {hipo}!={Math.Pow(sideA,2)+Math.Pow(sideB,2)}"));
            return res;
        }

        public override string ToString()
        {
            return base.ToString().Replace("@@","Perimeter")+$"\nArea: {Area}";
        }
    }
}