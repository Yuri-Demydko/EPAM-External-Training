using System;
using System.Collections.Generic;
using ConsoleGameDev.Entities.Abstract;
using ConsoleGameDev.Entities.Interfaces;
using ConsoleGameDev.Enums;
using ConsoleGameDev.Render.Interfaces;

namespace ConsoleGameDev.Entities
{
    public class Player : Entity, ICollector
    {
        private bool _locked = false;
        private int _ammo = 5;

        public string[] GetStatistics() => new[] {$"Ammo: {_ammo}"};
        protected override char[,] Points =>
            new[,]
            {
                {'.','O','.'},
                {'<','|','>'},
                {'/','"','\\'},
            };
        
        public bool Locked => _locked;

        public void Lock() => _locked = true;
        public Bullet? Shoot()
        {
            lock(this)
            {
                if (_ammo <= 0) return null;
                _ammo--;
                return new Bullet();
            }
        }

        public void CheckAndCollect<T>(IField field, MovingDirection md) where T : CollectableEntity
        { 
            var (x,y) = field.PositionOf(this.Key);
            var points = GetPoints();
            List<(int, int)> toCheck = new List<(int, int)>();
            try
            {
                switch (md)
                {
                    case MovingDirection.Up:
                    {
                        for (var i = 0; i < points.GetLength(0);i++)
                        {
                            toCheck.Add((x+i,y+points.GetLength(1)));
                        }

                        break;
                    }
                    case MovingDirection.Down:
                    {
                        for (var i = 0; i < points.GetLength(0);i++)
                        {
                            toCheck.Add((x+i,y-1));
                        }

                        break;
                    }
                    case MovingDirection.Right:
                    {
                        for (var i = 0; i < points.GetLength(1);i++)
                        {
                            toCheck.Add((x+points.GetLength(0),y+i));
                        }

                        break;
                    }
                    case MovingDirection.Left:
                    {
                        for (var i = 0; i < points.GetLength(1);i++)
                        {
                            toCheck.Add((x-1,y+i));
                        }

                        break;
                    }
                    case MovingDirection.None:
                    default:
                        throw new ArgumentOutOfRangeException(nameof(md), md, null);
                }
            }
            catch
            {
                //ignored
            }

            T ammo=null;
            foreach (var point in toCheck)
            {
                ammo = field.CheckPosition<T>(point.Item1, point.Item2);
                if (ammo != null) break;
            }

            if (ammo == null) return;
            field.RemoveObject(ammo);
            Buff<T>();
        }

        public void Buff<T>() where T : CollectableEntity
        {
            var buffs = new Dictionary<Type, Action>()
            {
                {typeof(Ammo), () => _ammo++},
            };
            buffs[typeof(T)].Invoke();
        }
    }
}