using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YGeometry
{
    public class Circumference : RoundBasedShape
    {
        public Circumference(Point center, double radius) : base(center, radius)
        {
        }
        

        
    }
}