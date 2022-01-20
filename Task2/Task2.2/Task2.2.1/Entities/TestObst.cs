using ConsoleGameDev.Entities.Abstract;

namespace ConsoleGameDev.Entities
{
    public class TestObst : Entity
    {
        protected override char[,] Points =>
            new[,]
            {
                {'0','1','2'},
                {'3','4','5'},
                {'@','7','8'},
            };
    }
}