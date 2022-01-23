using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace YGeometry
{
    public abstract class ShapeCreationHelper
    {
        protected delegate Shape ShapeCreationDelegate();

        public abstract Point[] CreatePoints(int instanceCount);
        public abstract Shape CreateShape<T>() where T : Shape;
    }
    
    public class ConsoleShapeCreationHelper:ShapeCreationHelper
    {
        public override Point[] CreatePoints(int instanceCount)
        {
            if (instanceCount < 1) throw new TargetParameterCountException("At least one point must be created!");
            var points = new Point[instanceCount];
            for (var i = 1; i <= instanceCount; i++)
            {
                Console.WriteLine($"Point #{i}");
                Console.Write("Enter X coordinate: ");
                var x = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter Y coordinate: ");
                var y = Convert.ToDouble(Console.ReadLine());
                points[i - 1] = new Point(x, y);
            }

            return points;
        }
        public override Shape CreateShape<T>()
        {
            if (typeof(T).IsAbstract)
                throw new ArgumentException(
                    $"Given type {nameof(T)} is abstract! U can't create instance of an abstract class!");

            Dictionary<Type, ShapeCreationDelegate> _typesToActionsTable = new()
            {
                {typeof(Circle), () => new Circle(CreatePoints(1)[0], AskRadius())},
                {typeof(Circumference), () => new Circumference(CreatePoints(1)[0], AskRadius())},
                {
                    typeof(Line), () =>
                    {
                        var p = CreatePoints(2);
                        return new Line(p[0], p[1]);
                    }
                },
                {
                    typeof(Rectangle), () =>
                    {
                        var p = CreatePoints(4);
                        return new Rectangle(p[0], p[1], p[2], p[3]);
                    }
                },
                {
                    typeof(Square), () =>
                    {
                        var p = CreatePoints(4);
                        return new Square(p[0], p[1], p[2], p[3]);
                    }
                },
                {
                    typeof(Triangle), () =>
                    {
                        var p = CreatePoints(3);
                        return new Triangle(p[0], p[1], p[2]);
                    }
                },
                {
                    typeof(Ring), () =>
                    {
                        var p = CreatePoints(1);
                        return new Ring(p[0], AskRadius(), AskInnerRadius<Ring>());
                    }
                }
            };

            var res = _typesToActionsTable[typeof(T)]();
            var results = new List<ValidationResult>();
            var context = new ValidationContext(res);
            if (!Validator.TryValidateObject(res, context, results, true))
            {
                StringBuilder errors = new StringBuilder("Shape validation errors:\n");
                
                foreach (var error in results)
                {
                    errors.Append(error.ErrorMessage+"\n");
                }

                throw new ValidationException(errors.ToString());
            }
            return res;

            static double AskRadius()
            {
                Console.Write($"Enter radius: ");
                return Convert.ToDouble(Console.ReadLine());
            }
            static double AskInnerRadius<T>() where T : Ring
            {
                Console.Write($"Enter inner radius of {typeof(T).Name}: ");
                return Convert.ToDouble(Console.ReadLine());
            }

        }
    }
    
}


    
