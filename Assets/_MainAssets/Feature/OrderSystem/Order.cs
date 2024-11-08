using System;
using System.Collections.Generic;
using CookSystem;

namespace OrderSystem
{
    [Serializable]
    public class Order
    {
        private List<FoodItemData> _orderedFood = new();
        public List<FoodItemData> allOrders = new();

        public bool IsOrderComplete => _orderedFood.Count == 0;

        public Action<List<FoodItemData>> onOrderComplete;
        public Action<FoodItemData> onOrderFulfilled;

        public Order(List<FoodItemData> order, Action<List<FoodItemData>> onOrderComplete = null,
            Action<FoodItemData> onOrderFulfilled = null)
        {
            allOrders.AddRange(order);
            _orderedFood.AddRange(order);
            this.onOrderComplete = onOrderComplete;
            this.onOrderFulfilled = onOrderFulfilled;
        }

        public bool TryReceive(FoodItemData item)
        {
            if (!CanReceive(item)) return false;
            _orderedFood.Remove(item);
            onOrderFulfilled?.Invoke(item);
            CheckOrder();
            return true;
        }

        private bool CanReceive(FoodItemData item)
        {
            return _orderedFood != null && _orderedFood.Contains(item);
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
            _orderedFood.Clear();
        }

        public FoodItemData GetNextFoodItem()
        {
            return _orderedFood[0];
        }
    }
}