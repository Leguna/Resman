using UnityEngine;

namespace CookSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Topping : MonoBehaviour
    {
        [SerializeField] private ToppingItemData allowedToppingItemData;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private ToppingItemData _toppingItemData;

        public ToppingItemData ToppingItemData => _toppingItemData;

        public void GetPicked()
        {
            spriteRenderer.gameObject.SetActive(false);
            _toppingItemData = null;
        }

        public float GetPrice()
        {
            return _toppingItemData.price;
        }

        public bool TryAddTopping(ToppingItemData toppingItemData)
        {
            if (toppingItemData != allowedToppingItemData) return false;
            _toppingItemData = toppingItemData;
            spriteRenderer.sprite = toppingItemData.icon;
            spriteRenderer.gameObject.SetActive(true);
            return true;
        }
    }
}