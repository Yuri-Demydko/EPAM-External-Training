namespace TASK3._3._3
{
    public interface IOrderNotificationSender
    {
        public void OrderNotify(Order order);
        public void MessageNotify(string error);
    }
}