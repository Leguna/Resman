namespace CookSystem
{
    public interface IToppingable
    {
        float GetTotalPrice();
        bool TryAddTopping(ToppingItemData topping);
    }

    public interface ITopping
    {
        public void AddToFood(IToppingable foodItem);
    }
}