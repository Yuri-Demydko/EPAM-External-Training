using System;
using System.IO;
using System.Threading;

namespace Task_4._1._1
{
    public class Program
    {
        public static void Main()
        {
            //place ur paths here
            var storage = "";
            var backup = "";
            var logDir = "";
            var watcher = new Watcher(storage,backup,logDir, new ConsoleNotificationChannel(), new ConsoleInputChannel());
            watcher.Start();
        }
    }
}