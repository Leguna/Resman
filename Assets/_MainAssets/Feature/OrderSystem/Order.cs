using System.Collections.Generic;
using CookSystem;
using UnityEngine;

namespace OrderSystem
{
    public class Order : MonoBehaviour
    {
        public FoodItemData foodItem;
        public List<string> requiredToppings;
        public float cookingTime;
        public float burnTime;
        public bool isReady;
        public bool isBurned;

        private float cookingTimer;
        private float burnTimer;

        public delegate void OrderStatusChanged(Order order);

        public event OrderStatusChanged OnOrderReady;
        public event OrderStatusChanged OnOrderBurned;

        private void Start()
        {
            StartCooking();
        }

        public void InitializeOrder(FoodItemData food, List<string> toppings)
        {
            foodItem = food;
            requiredToppings = toppings;
            cookingTime = food.cookingTime;
            burnTime = food.burnTime;
            cookingTimer = cookingTime;
            burnTimer = burnTime;
            isReady = false;
            isBurned = false;
        }

        private void StartCooking()
        {
            isReady = false;
            isBurned = false;
            cookingTimer = cookingTime;
        }

        private void Update()
        {
            if (!isReady && cookingTimer > 0)
            {
                cookingTimer -= Time.deltaTime;
                if (cookingTimer <= 0)
                {
                    isReady = true;
                    burnTimer = burnTime;
                    OnOrderReady?.Invoke(this);
                }
            }
            else if (isReady && !isBurned && burnTimer > 0)
            {
                burnTimer -= Time.deltaTime;
                if (burnTimer <= 0)
                {
                    isBurned = true;
                    OnOrderBurned?.Invoke(this);
                }
            }
        }
    }
}