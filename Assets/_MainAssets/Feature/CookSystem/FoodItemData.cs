using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace CookSystem
{
    [CreateAssetMenu(fileName = "FoodItem", menuName = "RestaurantGame/FoodItem")]
    public class FoodItemData : MyScriptableObject
    {
        public string itemName;
        public Color bgColor = Color.white;
        public float cookingTime = 5;
        public float burnTime = 10;
        public List<ToppingItemData> allowedToppings = new();
        public float price = 5;

        public bool IsBurnt(float time) => time >= burnTime;

        public float GetTotalPrice() => price + allowedToppings.Sum(topping => topping.price);

        protected override void OnValidate()
        {
            try
            {
                base.OnValidate();

                if (cookingTime < 0)
                    throw new ArgumentOutOfRangeException($"Cooking time cannot be negative");

                if (burnTime < 0)
                    throw new ArgumentOutOfRangeException($"Burn time cannot be negative");

                if (Math.Abs(burnTime - cookingTime) < 0.01)
                    throw new ArgumentOutOfRangeException($"Burn time cannot be equal to cooking time");

                if (price < 0)
                    throw new ArgumentOutOfRangeException($"Price cannot be negative");

                if (string.IsNullOrEmpty(itemName))
                    throw new ArgumentNullException($"Item name cannot be null or empty");

                if (burnTime < cookingTime)
                    throw new ArgumentOutOfRangeException($"Burn time cannot be less than cooking time");

                if (allowedToppings.Any(topping => topping == null))
                    throw new ArgumentNullException($"Topping cannot be null");
            }
            catch (Exception e)
            {
                Debug.LogError($"Validation Error in {name}: {e.Message}", this);
            }
        }
    }
}