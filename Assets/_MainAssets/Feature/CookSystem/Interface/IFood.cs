namespace CookSystem
{
    public interface IFood : IPutable<FoodItemData>, IServable<IPutable<FoodItemData>>
    {
    }

    public interface IPutable<in T>
    {
        void Put(T item);
    }

    public interface IServable<in T>
    {
        void Serve(T item);
    }
}