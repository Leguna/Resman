using System;
using System.Collections.Generic;
using CookSystem;

namespace OrderSystem
{
    [Serializable]
    public class Order : IReceive<FoodItemData>
    {
        private List<FoodItemData> foodItemData = new();
        public List<FoodItemData> allOrders = new();

        public bool IsOrderComplete => foodItemData.Count == 0;
        public bool CanReceive(FoodItemData item) => foodItemData.Contains(item);

        public Action<List<FoodItemData>> onOrderComplete;
        public Action<FoodItemData> onOrderFulfilled;

        public Order(List<FoodItemData> order, Action<List<FoodItemData>> onOrderComplete = null,
            Action<FoodItemData> onOrderFulfilled = null)
        {
            allOrders.AddRange(order);
            foodItemData.AddRange(order);
            this.onOrderComplete = onOrderComplete;
            this.onOrderFulfilled = onOrderFulfilled;
        }

        public bool TryReceive(FoodItemData item)
        {
            if (!CanReceive(item)) return false;
            foodItemData.Remove(item);
            onOrderFulfilled?.Invoke(item);
            CheckOrder();
            return true;
        }

        private void CheckOrder()
        {
            if (!IsOrderComplete) return;
            onOrderComplete?.Invoke(allOrders);
            CloseOrder();
        }

        public void CloseOrder()
        {
            allOrders.Clear();
            foodItemData.Clear();
            onOrderComplete = null;
            onOrderFulfilled = null;
        }
        
        public FoodItemData GetNextFoodItem()
        {
            return foodItemData[0];
        }
    }
}