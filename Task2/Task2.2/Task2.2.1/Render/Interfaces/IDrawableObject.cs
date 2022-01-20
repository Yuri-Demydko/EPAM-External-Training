using System;

namespace ConsoleGameDev.Render.Interfaces
{
    public interface IDrawableObject
    {
        public char[,] GetPoints();
        public Guid Key { get; }

    }
}