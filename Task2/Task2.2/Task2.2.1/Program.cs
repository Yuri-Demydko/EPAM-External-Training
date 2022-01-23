using System;

namespace ConsoleGameDev
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start\n" +
                              "If field doesn't render correctly press Q to force re-render");
            Console.ReadKey();
            var gameOrchestrator = new GameOrchestrator(20);
        }
    }
}