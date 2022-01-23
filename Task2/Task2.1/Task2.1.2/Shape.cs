using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;

namespace YGeometry
{
    public abstract class Shape: IValidatableObject
    {
        public abstract double TotalLength { get; } //aka Perimeter in RectBasedShapes or Triangles
        
        protected IEnumerable<Point> points;
        protected virtual int SuitablePointsNumber => 0;

        protected Shape(params Point[] points)
        {
            if (points.Length != SuitablePointsNumber)
                throw new TargetParameterCountException($"Suitable number of points for that shape: {SuitablePointsNumber}, you passed: {points.Length}");
            this.points = points.Take(SuitablePointsNumber);
        }
        
        
        public override string ToString()
        {
            var res = $"\t{GetType().Name}:\n" +
                      $"@@: {TotalLength}";
            return res;
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}