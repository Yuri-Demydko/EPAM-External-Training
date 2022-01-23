using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace YGeometry
{
    public class Ring : RoundBasedShape,IAreaCalculatable
    {
        private double _innerRadius;
    
        public double InnerLength => 2 * Math.PI * _innerRadius;
        public double OuterLength => 2 * Math.PI * Radius;
        
        public override double TotalLength => InnerLength + OuterLength;
        
        public Ring(Point center, double radius, double innerRadius) : base(center, radius)
        {
            _innerRadius = innerRadius;
        }

        public double Area => Math.PI * Math.Pow(Radius - InnerLength, 2);
    
        public double InnerRadius => _innerRadius;
        
        public override string ToString()
        {
            return base.ToString()+$"\nInner radius: {InnerRadius}\nArea: {Area}";
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var res= base.Validate(validationContext).ToList();
            if (_innerRadius <= 0) res.Add(new ValidationResult("Inner radius must be positive number!"));
            return res;
        }
    }
}