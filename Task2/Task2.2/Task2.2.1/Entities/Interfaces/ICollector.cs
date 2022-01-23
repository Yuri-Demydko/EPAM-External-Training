using ConsoleGameDev.Entities.Abstract;
using ConsoleGameDev.Enums;
using ConsoleGameDev.Render.Interfaces;

namespace ConsoleGameDev.Entities.Interfaces
{
    public interface ICollector
    {
        public void CheckAndCollect<T>(IField field,MovingDirection md) where T : CollectableEntity;
        public void Buff<T>() where T : CollectableEntity;
    }
}