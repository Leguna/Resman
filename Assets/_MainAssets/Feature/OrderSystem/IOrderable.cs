namespace OrderSystem
{
    public interface IOrderable
    {
        void ProcessOrder(Order order);
        void ServeOrder(Order order);
    }
}