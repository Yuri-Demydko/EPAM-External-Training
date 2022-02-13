namespace TASK3._3._3
{
    public class Order
    {
        
        public Order(int number, int pizzaCode)
        {
            Number = number;
            PizzaCode = pizzaCode;
            _state = OrderState.Placed;
        }

        public void NextState() => 
            _state = _state == OrderState.Placed ? OrderState.Processing : OrderState.Done;

        private OrderState _state;
        public OrderState State => _state;
        public int Number { get; }
        public int PizzaCode { get; }

        public override string ToString()
        {
            return $"Order #{Number} ({OrderSystem.PizzaDictionary[PizzaCode]}) {State}";
        }
    }
}