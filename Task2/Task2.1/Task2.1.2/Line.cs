using System.Linq;

namespace YGeometry
{
    public class Line : Shape
    {
        public override double TotalLength => 
            Point.DistanceBetween(this.points.First(), this.points.Last());

        public Line(Point p1, Point p2):base(p1, p2)
        {
        }

        protected override int SuitablePointsNumber => 2;
        

        public override string ToString()
        {
            return base.ToString().Replace("@@","Length");
        }
    }
}