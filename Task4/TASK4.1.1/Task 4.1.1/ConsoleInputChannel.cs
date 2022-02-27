using System;

namespace Task_4._1._1
{
    public class ConsoleInputChannel:IInputChannel
    {
        public string GetStringInput()
        {
            return Console.ReadLine();
        }
    }
}