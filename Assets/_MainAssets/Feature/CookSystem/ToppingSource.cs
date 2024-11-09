using System.Collections.Generic;
using Touch;
using UnityEngine;

namespace CookSystem
{
    public class ToppingSource : MonoBehaviour, ITouchable
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private List<FoodPlate> foodPlates;
        [SerializeField] private ToppingItemData toppingItemData;

        private void Awake()
        {
            Init(toppingItemData);
        }

        private void Init(ToppingItemData toppingItemData)
        {
            this.toppingItemData = toppingItemData;
            icon.sprite = toppingItemData.icon;
            Hide();
        }

        public void Show()
        {
            icon.gameObject.SetActive(true);
        }

        public void Hide()
        {
            icon.gameObject.SetActive(false);
        }

        public void OnTouch()
        {
            AddToAvailableFoodPlate();
        }

        private void AddToAvailableFoodPlate()
        {
            foreach (var foodPlate in foodPlates)
            {
                if (!foodPlate.TryAddTopping(toppingItemData)) continue;
                break;
            }
        }
    }
}