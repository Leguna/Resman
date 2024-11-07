namespace CookSystem
{
    public interface IFood : IReceiver<FoodItemData>, IServable
    {
    }

    public interface IReceiver<in T>
    {
        bool Receive(T item);
    }

    public interface IServable
    {
        void Serve();
    }

}