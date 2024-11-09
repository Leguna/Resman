using System;
using System.Collections.Generic;
using CookSystem;

namespace CustomerSystem.OrderSystem
{
    [Serializable]
    public class Order
    {
        public List<OrderedFoodItemData> allOrders;

        public bool IsOrderComplete => allOrders.Count == 0;

        public Action<List<OrderedFoodItemData>> onOrderComplete;
        public Action<OrderedFoodItemData> onOrderFulfilled;

        public Order(List<OrderedFoodItemData> order, Action<List<OrderedFoodItemData>> onOrderComplete = null,
            Action<OrderedFoodItemData> onOrderFulfilled = null)
        {
            allOrders = order;
            this.onOrderComplete = onOrderComplete;
            this.onOrderFulfilled = onOrderFulfilled;
        }

        public OrderedFoodItemData CanReceive(OrderedFoodItemData item)
        {
            foreach (var orderedFood in allOrders)
            {
                if (orderedFood.foodItemData != item.foodItemData) continue;
                if (orderedFood.toppingItemData.Count != item.toppingItemData.Count) continue;
                var isSame = true;
                for (var i = 0; i < orderedFood.toppingItemData.Count; i++)
                {
                    if (orderedFood.toppingItemData[i] == item.toppingItemData[i]) continue;
                    isSame = false;
                    break;
                }

                if (!isSame) continue;
                return orderedFood;
            }

            return null;
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
        }

        public int Receive(OrderedFoodItemData foodPlateOrderedFoodItemData)
        {
            var totalPrice = foodPlateOrderedFoodItemData.GetTotalPrice();
            allOrders.Remove(foodPlateOrderedFoodItemData);
            CheckOrder();
            return totalPrice;
        }
    }

    public class OrderedFoodItemData
    {
        public FoodItemData foodItemData;
        public List<ToppingItemData> toppingItemData = new();

        public OrderedFoodItemData(FoodItemData foodItemData, List<ToppingItemData> toppingItemData)
        {
            this.foodItemData = foodItemData;
            this.toppingItemData = toppingItemData;
        }

        public OrderedFoodItemData(FoodItemData foodItemData)
        {
            this.foodItemData = foodItemData;
        }

        public bool TryAddTopping(ToppingItemData toppingItemData)
        {
            if (this.toppingItemData.Contains(toppingItemData)) return false;
            if (!foodItemData.allowedToppings.Contains(toppingItemData)) return false;
            this.toppingItemData.Add(toppingItemData);
            return true;
        }

        public int GetTotalPrice()
        {
            var totalPrice = foodItemData.price;
            foreach (var topping in toppingItemData)
            {
                totalPrice += topping.price;
            }

            return totalPrice;
        }
    }
}