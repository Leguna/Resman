using System;
using Core;
using UnityEngine;

namespace CookSystem
{
    [CreateAssetMenu(fileName = "ToppingItemData", menuName = "RestaurantGame/ToppingItemData")]
    public class ToppingItemData : MyScriptableObject
    {
        public string toppingName;
        public Color bgColor = Color.white;
        public float price = 2;

        protected override void OnValidate()
        {
            try
            {
                base.OnValidate();

                if (price < 0)
                    throw new ArgumentOutOfRangeException($"Price cannot be negative");

                if (string.IsNullOrEmpty(toppingName))
                    throw new ArgumentNullException($"Topping name cannot be null or empty");
            }
            catch (Exception e)
            {
                Debug.LogError($"Validation Error in {name}: {e.Message}", this);
            }
        }
    }
}