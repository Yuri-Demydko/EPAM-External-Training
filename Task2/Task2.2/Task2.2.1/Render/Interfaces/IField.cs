using System;
using System.Collections.Generic;

namespace ConsoleGameDev.Render.Interfaces
{
    public interface IField
    {
        public char EmptyCellSymbol { get; }
        public int Size { get; }
        public void PlaceObject(IDrawableObject dobj,int x, int y);
        public void MoveObject(IDrawableObject dobj,int x, int y);
        public void RemoveObject(IDrawableObject dobj);
        public (int, int) PositionOf(Guid objKey);
        public List<(int, int)> AllPointsOf(Guid objKey);
        public T? CheckPosition<T>(int x, int y) where T : class, IDrawableObject;
        public void DrawSnapshotDebug();
        public void DrawTextInformation(string[] information);
    }
}