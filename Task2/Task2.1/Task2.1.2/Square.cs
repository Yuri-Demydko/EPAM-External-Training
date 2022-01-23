using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace YGeometry
{
    public class Square:RectBasedShape
    {
        public Square(Point p1, Point p2, Point p3, Point p4) : base(p1, p2, p3, p4)
        {
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var res= base.Validate(validationContext).ToList();
            var sideA = points.ElementAt(0).DistanceTo(points.ElementAt(1));
            var sideC = points.ElementAt(2).DistanceTo(points.ElementAt(3));
            if(sideA!=sideC) res.Add(new ValidationResult("Sides must be equal!"));
            return res;
        }
    }
}