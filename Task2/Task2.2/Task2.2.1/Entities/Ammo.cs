using ConsoleGameDev.Entities.Abstract;
using ConsoleGameDev.Entities.Interfaces;

namespace ConsoleGameDev.Entities
{
    public class Ammo : CollectableEntity
    {
        public Ammo(ICollector collector) : base(collector)
        {
        }

        protected override char[,] Points => new[,]
        {
            {'1'}
        };
    }
}