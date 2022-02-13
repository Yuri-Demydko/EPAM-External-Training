using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TASK3._3._3
{
    public class ConsoleOrderNotificationSender:IOrderNotificationSender
    {
        public void OrderNotify(Order order)
        {
            Console.WriteLine(order.ToString());
        }

        public void MessageNotify(string error)
        {
            Console.WriteLine(error);
        }
    }
}