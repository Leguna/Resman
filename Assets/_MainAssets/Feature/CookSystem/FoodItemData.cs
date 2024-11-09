using System;
using System.Collections.Generic;
using System.Linq;
using OrderSystem;
using UnityEngine;

namespace CookSystem
{
    [CreateAssetMenu(fileName = "FoodItem", menuName = "RestaurantGame/FoodItem")]
    public class FoodItemData : MyScriptableObject, IOrderable
    {
        public string itemName;
        public Sprite icon;
        public List<ToppingItemData> allowedToppings = new();
        public int price = 5;

        private bool CanAddTopping(ToppingItemData topping) => allowedToppings.Contains(topping);

        protected override void OnValidate()
        {
            TryCatchWrapper(() =>
            {
                base.OnValidate();
                if (price < 0)
                    throw new ArgumentOutOfRangeException($"Price cannot be negative");

                if (string.IsNullOrEmpty(itemName))
                    throw new ArgumentNullException($"Item name cannot be null or empty");

                if (allowedToppings.Any(topping => topping == null))
                    throw new ArgumentNullException($"Topping cannot be null");
            });
        }
    }
}