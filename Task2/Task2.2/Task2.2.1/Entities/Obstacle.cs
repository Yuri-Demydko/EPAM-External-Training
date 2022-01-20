using ConsoleGameDev.Entities.Abstract;

namespace ConsoleGameDev.Entities
{
    public class Obstacle : Entity
    {
        protected override char[,] Points =>
            new[,]
            {
                {'X','X','X'},
                {'X','X','X'},
                {'X','X','X'},
            };
    }
}