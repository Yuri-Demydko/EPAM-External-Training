using System;
using System.Collections.Generic;
using System.Linq;

namespace YGeometry
{
    public class CustomPaint
    {
        private string _userName;
        private readonly List<Shape> _shapes;

        public string UserName => _userName;
        public CustomPaint(string userName)
        {
            _userName = userName;
            _shapes = new List<Shape>();
        }
        public static CustomPaint InitCustomPaint()
        {
            Console.Write("Enter user's name: ");
            return new CustomPaint(Console.ReadLine());
        }

        public void AddShape<T>() where T: Shape, new()
        {
            _shapes.Add(new T());
        }
        
       public void DrawShapes() => _shapes.ForEach(s=>Console.WriteLine($"{s}\n{new string('-',s.ToString().Split("\n").Last().Length)}"));
       
    }
}