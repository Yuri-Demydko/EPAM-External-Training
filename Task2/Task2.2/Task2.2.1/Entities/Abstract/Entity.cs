#nullable enable
using System;
using ConsoleGameDev.Render.Interfaces;

namespace ConsoleGameDev.Entities.Abstract
{
    public abstract class Entity:IDrawableObject
    {
        protected abstract char[,] Points { get; }
        
        public char[,] GetPoints()
        {
            var res = new char[Points.GetLength(0), Points.GetLength(1)];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = res.GetLength(1) - 1; j >= 0; j--)
                {
                    res[res.GetLength(0)-i-1, j] = Points[i, j];
                }
            }
            return res;
        }

        private Guid _key;
        protected Entity()
        {
            _key=Guid.NewGuid();
        }
        public Guid Key => _key;
        
    }
}