using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using Task3._3._1;
using TASK3._3._2;
using TASK3._3._3;

namespace Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            var run1 = false;
            var run2 = true;
            var run3 = false;

            if (run1)
            {
                var test = new[] {1, 2, 3, 3, 3, 3};
                Console.WriteLine(test.YMostCommon());
                test.YApplyFunction(i => ++i);
                test.ToList().ForEach(Console.WriteLine);
            }
            
            if(run2)
            {
                Console.WriteLine("Task 3.2 test cases:");
                var tests = new List<string> {"Русская строка!", "Eng str!", "F3Ck", "010"};
                tests.ForEach(i => Console.WriteLine($"{i}: {i.GetLangType()}"));
                
            }

            if (run3)
            {
                OrderSystem pizzeria = new OrderSystem(new ConsoleOrderNotificationSender());
                pizzeria.StartWork();
                
                var stop = false;

                Timer timer = new Timer();
                timer.Interval = 6000;
                timer.Elapsed += (o, args) => { stop = true; timer.Stop();};
                timer.Start();
                Console.WriteLine("---WE ARE OPEN. PLACE YOUR ORDERS---");
                do
                {
                    if (stop) break;
                    var pizza = Console.ReadLine();
                    pizzeria.PlaceOrder(pizza);
                } while (!stop);
                pizzeria.StopWork();
            }
         
        }
    }
}