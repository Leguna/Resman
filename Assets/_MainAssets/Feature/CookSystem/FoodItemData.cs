using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CookSystem
{
    [CreateAssetMenu(fileName = "FoodItem", menuName = "RestaurantGame/FoodItem")]
    public class FoodItemData : MyScriptableObject
    {
        public string itemName;
        public Color bgColor = Color.white;
        public List<ToppingItemData> allowedToppings = new();
        public float price = 5;
        [Header("Runtime Data")] public List<ToppingItemData> addedToppings = new();

        public float GetTotalPrice() => price + addedToppings.Sum(topping => topping.price);

        private bool CanAddTopping(ToppingItemData topping) => allowedToppings.Contains(topping);

        public bool TryAddTopping(ToppingItemData topping)
        {
            if (!CanAddTopping(topping))
                return false;
            addedToppings.Add(topping);
            return true;
        }

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