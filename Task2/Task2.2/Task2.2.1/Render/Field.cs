#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleGameDev.Enums;
using ConsoleGameDev.Render.Interfaces;

namespace ConsoleGameDev.Render
{
    public class Field:IField
    {
        private readonly IRenderer _renderer;
        private readonly int _size;
        private readonly Dictionary<IDrawableObject, (int, int)> _objects;
        private readonly char[,] _shapshot;
            
        public Field(int size, IRenderer renderer)
        {
            _size = size;
            this._renderer = renderer;
            _renderer.ClearField(this);
            _objects = new Dictionary<IDrawableObject, (int, int)>();
            _shapshot = new char[size, size];
        }
        public char EmptyCellSymbol => '_';
        public int Size => _size;
        
        private void CheckObject(IDrawableObject dobj,bool needExist)
        {
            var result = _objects.Any(x => x.Key.Key == dobj.Key);
            switch (needExist)
            {
                case true when !result:
                    throw new KeyNotFoundException("Object with such key doesn't exist!");
                case false when result:
                    throw new Exception("Object with the same key already exist in that field!");
            }
        }
        public void PlaceObject(IDrawableObject dobj, int x, int y)
        {
            CheckObject(dobj, false);
            var points = dobj.GetPoints();
            for (var i = 0; i < points.GetLength(0); i++)
            {
                for (var j = 0; j < points.GetLength(1); j++)
                {
                    _renderer.DrawCell(x+i,y+j,points[j,i],this);
                    try
                    {
                        _shapshot[x + i, y + j] = points[j, i];
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            _objects.Add(dobj,(x,y));
        }
        
        private bool CheckMove(Guid dobjKey, MovingDirection md)
        {
            var select = _objects.Where(i => i.Key.Key == dobjKey)
                .Select(i=>(i.Key,i.Value.Item1,i.Value.Item2)).First();
            var dobj = select.Key;
            var (x, y) = (select.Item2, select.Item3);
            var points = dobj.GetPoints();
            try
            {
                switch (md)
                {
                    case MovingDirection.Up:
                    {
                        var ok = true;
                        for (var i = 0; i < points.GetLength(0);i++)
                        {
                            ok= _shapshot[x+i, y + points.GetLength(1)] == '\0' ||
                                _shapshot[x+i, y + points.GetLength(1)] == EmptyCellSymbol;
                            if (!ok) return false;
                        }
                        return ok;
                    }
                    case MovingDirection.Down:
                    {
                        var ok = true;
                        for (var i = 0; i < points.GetLength(0);i++)
                        {
                            ok = _shapshot[x + i, y - 1] == '\0' || _shapshot[x + i, y - 1] == EmptyCellSymbol;
                            if (!ok) return false;
                        }
                        return ok;
                    }
                    case MovingDirection.Right:
                    {
                        var ok = true;
                        for (var i = 0; i < points.GetLength(1);i++)
                        {
                           ok= _shapshot[x + points.GetLength(0), y+i] == '\0' ||
                                _shapshot[x + points.GetLength(0), y+i] == EmptyCellSymbol;
                           if (!ok) return false;
                        }

                        return ok;
                    }
                    case MovingDirection.Left:
                    {
                        var ok = true;
                        for (var i = 0; i < points.GetLength(1);i++)
                        {
                            ok= _shapshot[x - 1, y+i] == '\0' || _shapshot[x - 1, y+i] == EmptyCellSymbol;
                            if (!ok) return false;
                        }

                        return ok;
                    }
                    case MovingDirection.None:
                    default:
                        throw new ArgumentOutOfRangeException(nameof(md), md, null);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void MoveObject(IDrawableObject dobj, int x, int y)
        {
            CheckObject(dobj,true);
            var (deltaX, deltaY) = (x - _objects[dobj].Item1, y - _objects[dobj].Item2);
            var md = MovingDirection.None;
            if(deltaY==0)
            {
                if (deltaX > 0)
                    md = MovingDirection.Right;
                else
                    md = MovingDirection.Left;
            }
            if(deltaX==0)
            {
                if (deltaY > 0)
                    md = MovingDirection.Up;
                else
                    md = MovingDirection.Down;
            }
            if (!CheckMove(dobj.Key, md)) return;
            RemoveObject(dobj);
            PlaceObject(dobj,x,y);
        }
        public void RemoveObject(IDrawableObject dobj)
        {
            CheckObject(dobj,true);
            var (x, y) = _objects[dobj];
            var points = dobj.GetPoints();
            for (var i = 0; i < points.GetLength(0); i++)
            {
                for (var j = 0; j < points.GetLength(1); j++)
                {
                    _renderer.DrawCell(x+i,y+j,EmptyCellSymbol,this);
                    try
                    {
                        _shapshot[x + i, y + j] = EmptyCellSymbol;
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            _objects.Remove(dobj);
        }
        public (int, int) PositionOf(Guid objKey)
        {
            return _objects.First(x => x.Key.Key == objKey).Value;
        }

        public List<(int, int)> AllPointsOf(Guid objKey)
        {
            var kvp = _objects.First(x => x.Key.Key == objKey);
            var (x, y) = kvp.Value;
            var obj = kvp.Key;
            var points = obj.GetPoints();
            var w = points.GetLength(0);
            var h = points.GetLength(1);
            var res = new List<(int, int)>();
            for (var i = x; i < x + w; i++)
            {
                for (var j = y; j < y + h; j++)
                {
                    res.Add((i,j));
                }
            }

            return res;
        }

        public T? CheckPosition<T>(int x, int y) where T : class, IDrawableObject
        {
            return _objects
                .Where(i => AllPointsOf(i.Key.Key).Contains((x, y)))
                .Select(i=>i.Key)
                .OfType<T>().FirstOrDefault();
        }

        public void DrawSnapshotDebug()
        {
            Console.Clear();
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _renderer.DrawCell(i,j,_shapshot[i,j]!='\0'?_shapshot[i,j]:this.EmptyCellSymbol,this);
                }
            }
        }

        public void DrawTextInformation(string[] information)
        {
            foreach (var info in information)
            {
                _renderer.DrawText(info,this);
            }
        }
    }
}