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

        [SerializeField]
        private void Awake()
        {
            _animate = GetComponent<IAnimate>();
        }

        public void Serve()
        {
            if (_foodItemData == null) return;
            _foodItemData = null;
            foodSpriteRenderer.gameObject.SetActive(false);
            _animate.PlayAnimation();
        }

        public bool Receive(FoodItemData item)
        {
            if (_foodItemData != null) return false;
            if (item != allowedFoodItemData) return false;
            _foodItemData = item;
            foodSpriteRenderer.gameObject.SetActive(true);
            UpdateUI();
            return true;
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

        public void OnTouch() => Serve();

        public void OnDoubleTap() => RemoveFoodItem();
    }
}