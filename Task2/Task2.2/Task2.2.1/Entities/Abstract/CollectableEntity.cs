using ConsoleGameDev.Entities.Interfaces;

namespace ConsoleGameDev.Entities.Abstract
{
    public abstract class CollectableEntity:Entity
    {
        protected ICollector Collector;

        protected CollectableEntity(ICollector collector)
        {
            this.Collector = collector;
        }
        //protected abstract TimeSpan 
    }
}