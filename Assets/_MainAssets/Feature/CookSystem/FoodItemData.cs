using System;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "FoodItem", menuName = "RestaurantGame/FoodItem")]
    public class FoodItemData : ScriptableObject
    {
        public string id;
        public string itemName;
        public Color bgColor = Color.white;
        public float cookingTime = 5;
        public float burnTime = 10;
        public List<ToppingItemData> allowedToppings = new();
        public float price = 5;

        private void OnValidate()
        {
            if (cookingTime < 0)
                throw new ArgumentOutOfRangeException($"Cooking time cannot be negative");

            if (burnTime < 0)
                throw new ArgumentOutOfRangeException($"Burn time cannot be negative");

            if (price < 0)
                throw new ArgumentOutOfRangeException($"Price cannot be negative");

            if (string.IsNullOrEmpty(itemName))
                throw new ArgumentNullException($"Item name cannot be null or empty");

            if (burnTime < cookingTime)
                throw new ArgumentOutOfRangeException($"Burn time cannot be less than cooking time");

            if (string.IsNullOrEmpty(id)) id = Guid.NewGuid().ToString();
        }
    }
}