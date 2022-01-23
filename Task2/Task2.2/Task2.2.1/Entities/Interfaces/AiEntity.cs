using System;
using ConsoleGameDev.Entities.Abstract;
using ConsoleGameDev.Render.Interfaces;

namespace ConsoleGameDev.Entities.Interfaces
{
    public abstract class AiEntity:Entity
    {
        protected abstract TimeSpan SleepTime { get; }
        protected abstract TimeSpan LifeTime { get; }
        protected abstract (int, int) CalculateNextStep(IField field);

        public abstract void DoLifeCycle(IField field);

        protected readonly Entity Target;
        protected AiEntity(Entity target)
        {
            this.Target = target;
        }
    }
}