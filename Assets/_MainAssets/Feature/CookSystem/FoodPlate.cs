using System;
using System.Collections.Generic;
using Animation;
using CustomerSystem.OrderSystem;
using Touch;
using UnityEngine;

namespace CookSystem
{
    public class FoodPlate : MonoBehaviour, ITouchable, IOnDoubleTap, IFood
    {
        public FoodItemData foodItemData;
        private IAnimate _animate;
        [SerializeField] private FoodItemData allowedFoodItemData;
        [SerializeField] private SpriteRenderer foodSpriteRenderer;
        [SerializeField] private List<Topping> toppings;

        private Action<FoodPlate> _onPlateServe;

        public OrderedFoodItemData OrderedFoodItemData()
        {
            var toppingsData = new List<ToppingItemData>();
            foreach (var topping in toppings)
            {
                if (topping.ToppingItemData == null) continue;
                toppingsData.Add(topping.ToppingItemData);
            }

            return new OrderedFoodItemData(foodItemData, toppingsData);
        }

        private void Awake()
        {
            if (allowedFoodItemData == null)
            {
                Debug.LogError("Allowed food item data is not set", this);
            }

            _animate = GetComponent<IAnimate>();
        }

        public void Init(Action<FoodPlate> onPlateServe)
        {
            foodItemData = null;
            _onPlateServe = onPlateServe;
            foodSpriteRenderer.gameObject.SetActive(false);
        }

        public void TrySend()
        {
            if (foodItemData == null) return;
            _onPlateServe?.Invoke(this);
        }

        public bool TryReceive(FoodItemData item)
        {
            if (!CanReceive(item)) return false;
            foodItemData = item;
            foodSpriteRenderer.gameObject.SetActive(true);
            UpdateUI();
            return true;
        }

        public bool CanReceive(FoodItemData item)
        {
            if (foodItemData != null) return false;
            return item == allowedFoodItemData;
        }

        public bool TryAddTopping(ToppingItemData toppingItemData)
        {
            if (AlreadyHasTopping(toppingItemData)) return false;
            foreach (var topping in toppings)
            {
                if (topping.TryAddTopping(toppingItemData))
                {
                    return true;
                }
            }

            return false;
        }

        private bool AlreadyHasTopping(ToppingItemData toppingItemData)
        {
            return toppings.Exists(topping => topping.ToppingItemData == toppingItemData);
        }

        private void UpdateUI()
        {
            foodSpriteRenderer.sprite = foodItemData.icon;
            _animate.PlayAnimation();
        }

        public void CleanPlate()
        {
            foreach (var topping in toppings) topping.GetPicked();
            if (foodItemData == null) return;
            foodItemData = null;
            foodSpriteRenderer.gameObject.SetActive(false);
            _animate.PlayAnimation();
        }

        public void OnTouch() => TrySend();

        public void OnDoubleTap() => CleanPlate();
    }
}