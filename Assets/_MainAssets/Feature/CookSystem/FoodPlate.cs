using System;
using Animation;
using Touch;
using UnityEngine;

namespace CookSystem
{
    public class FoodPlate : MonoBehaviour, ITouchable, IOnDoubleTap, IFood
    {
        private FoodItemData _foodItemData;
        private IAnimate _animate;
        [SerializeField] private FoodItemData allowedFoodItemData;
        [SerializeField] private SpriteRenderer foodSpriteRenderer;

        private Action<FoodItemData, FoodPlate> _onPlateServe;

        private void Awake()
        {
            _animate = GetComponent<IAnimate>();
        }

        public void Init(Action<FoodItemData, FoodPlate> onPlateServe)
        {
            _foodItemData = null;
            _onPlateServe = onPlateServe;
            foodSpriteRenderer.gameObject.SetActive(false);
        }

        public void TrySend()
        {
            if (_foodItemData == null) return;
            _onPlateServe?.Invoke(_foodItemData, this);
        }


        public void RemoveFood()
        {
            _foodItemData = null;
            foodSpriteRenderer.gameObject.SetActive(false);
            _animate.PlayAnimation();
        }

        public bool TryReceive(FoodItemData item)
        {
            if (!CanReceive(item)) return false;
            _foodItemData = item;
            foodSpriteRenderer.gameObject.SetActive(true);
            UpdateUI();
            return true;
        }

        public bool CanReceive(FoodItemData item)
        {
            if (_foodItemData != null) return false;
            return item == allowedFoodItemData;
        }

        private void UpdateUI()
        {
            foodSpriteRenderer.sprite = _foodItemData.icon;
            _animate.PlayAnimation();
        }

        private void RemoveFoodItem()
        {
            if (_foodItemData == null) return;
            _foodItemData = null;
            foodSpriteRenderer.gameObject.SetActive(false);
            _animate.PlayAnimation();
        }

        public void OnTouch() => TrySend();

        public void OnDoubleTap() => RemoveFoodItem();
    }
}