using System;

namespace Task_4._1._1
{
    public class ConsoleNotificationChannel:INotificationChannel
    {
        public void Notify(string message)
        {
            Console.WriteLine(message);
        }
    }
}