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

        protected RectBasedShape():base()
        {
            
        }

        public override string ToString()
        {
            return base.ToString().Replace("@@","Perimeter")+$"\nArea: {Area}";
        }
    }
}