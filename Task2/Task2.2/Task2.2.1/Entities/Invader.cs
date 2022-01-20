using System;
using System.Linq;
using System.Threading;
using ConsoleGameDev.Entities.Abstract;
using ConsoleGameDev.Entities.Interfaces;
using ConsoleGameDev.Render.Interfaces;
using Timer = System.Timers.Timer;

namespace ConsoleGameDev.Entities
{
    public class Invader : AiEntity
    {
        protected override char[,] Points =>
            new[,]
            {
                {'}','O','{'},
                {'}','A','{'},
                {'@','V','@'},
            };

        private int _hp=10;
        private bool _isAlive = true;
        public void Hurt(int value)
        {
            _hp -= value;
            if (_hp <= 0)
                _isAlive = false;
        }

        protected override TimeSpan SleepTime =>TimeSpan.FromSeconds(0.4);
        protected override TimeSpan LifeTime =>TimeSpan.FromSeconds(60);

        public bool IsAlive => _isAlive;

        protected override (int, int) CalculateNextStep(IField field)
        {
            var curPos = field.PositionOf(Key);
            var deltaX = (field.PositionOf(this.Target.Key).Item1 - curPos.Item1) > 0 ? 1 : -1;
            var deltaY = (field.PositionOf(this.Target.Key).Item2 - curPos.Item2) > 0 ? 1 : -1;
            var rand = new Random().Next(0, 2) == 1;
            return rand ? (curPos.Item1, curPos.Item2 +deltaY) : (curPos.Item1+deltaX, curPos.Item2);
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
                    var targetPointSet = field.AllPointsOf(Target.Key);
                    if(targetPointSet.Any(a=> Math.Abs(x-a.Item1)<=1&&Math.Abs(y-a.Item2)<=1))
                    {
                        (Target as Player)?.Lock();
                        field.RemoveObject(Target);
                    }
                    Thread.Sleep(SleepTime);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public Invader(Entity target) : base(target)
        {
        }
    }
}