using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TASK3._3._3
{
    public class OrderSystem
    {
        private IOrderNotificationSender NotificationSender;
        public static readonly Dictionary<int, string> PizzaDictionary = new Dictionary<int, string>()
        {
            {1, "4cheese"},
            {2, "margarita"},
            {3, "peperoni"}
        };

        private int ordersCount = 0;
        private ConcurrentQueue<Order> OrderQueue=new ConcurrentQueue<Order>();

        public OrderSystem(IOrderNotificationSender notificationSender)
        {
            NotificationSender = notificationSender;
        }

        public void StartWork()
        {
            working = true;
            orderAble = true;
        }

        public void StopWork()
        {
            orderAble = false;
            NotificationSender.MessageNotify("We are closed for new orders. Placed orders will get done!");
            while (!OrderQueue.IsEmpty||currentOrder!=null)
                continue;
            working = false;
        }

        private bool orderAble = false;
        private bool working=false;
        public void PlaceOrder(string pizzaName)
        {
            if (!working||!orderAble)
            {
                NotificationSender.MessageNotify("We are closed!");
                return;
            }
            try
            {
                int pCode = PizzaDictionary
                    .First(i => i.Value == pizzaName)
                    .Key;
                var order = new Order(ordersCount + 1, pCode);
                NotificationSender.OrderNotify(order);
                OrderQueue.Enqueue(order);
                ordersCount++;
                if (ordersCount == 1)
                    ProcessOrders();
            }
            catch (Exception)
            {
                NotificationSender.MessageNotify("We haven't such pizza!");
            }
        }

        private Thread ProccessingThread;
        private Order currentOrder;
        private void ProcessOrders()
        {
            this.ProccessingThread = new Thread(
                () =>
                {
                    while(working)
                    {
                        if(OrderQueue.IsEmpty) continue;
                        OrderQueue.TryDequeue(out currentOrder);
                        if(currentOrder==null)
                            continue;
                        currentOrder.NextState();
                        NotificationSender.OrderNotify(currentOrder);
                        Thread.Sleep(new Random().Next(1000, 5000));
                        currentOrder.NextState();
                        NotificationSender.OrderNotify(currentOrder);
                        currentOrder = null;
                    }
                }
            );
            ProccessingThread.Start();
        }
        
        
    }
}