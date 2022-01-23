using System;
using System.Threading;
using ConsoleGameDev.Entities.Abstract;
using ConsoleGameDev.Entities.Interfaces;
using ConsoleGameDev.Render.Interfaces;
using Timer = System.Timers.Timer;

namespace ConsoleGameDev.Entities
{
    public class Bullet : AiEntity
    {
        
        protected override char[,] Points => new [,] {{'^'}};
        protected override TimeSpan SleepTime =>TimeSpan.FromSeconds(0.125/2);
        protected override TimeSpan LifeTime =>TimeSpan.FromSeconds(1);

        private int _damage = 2;

        protected override (int, int) CalculateNextStep(IField field)
        {
            var curPos = field.PositionOf(Key);
            return (curPos.Item1, curPos.Item2 + 1);
        }

        public override void DoLifeCycle(IField field)
        {
            try
            {
                var end = false;
                var timer = new Timer(LifeTime.TotalMilliseconds);
                timer.Elapsed += (_, _) =>
                {
                    end = true;
                    Thread.Sleep(SleepTime);
                    field.RemoveObject(this);
                };
                timer.Start();
                while (!end)
                {
                    var (x, y) = CalculateNextStep(field);
                    field.MoveObject(this, x, y);
                    var found = field.CheckPosition<Invader>(x,y);
                    if (found != null)
                    {
                        found.Hurt(_damage);
                        field.RemoveObject(this);
                        if(!found.IsAlive) field.RemoveObject(found);
                        break;
                    }
                    Thread.Sleep(SleepTime);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public Bullet(Entity target) : base(target)
        {
        }

        public Bullet() : base(null)
        {
            
        }
    }
}