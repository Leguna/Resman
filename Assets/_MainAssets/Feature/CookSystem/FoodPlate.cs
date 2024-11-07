using Animation;
using Touch;
using UnityEngine;

namespace CookSystem
{
    public class FoodPlate : MonoBehaviour, ITouchable, IFood
    {
        private FoodItemData foodItemData;
        [SerializeField] private FoodItemData allowedFoodItemData;
        private IAnimate _animate;

        private void Awake()
        {
            _animate = GetComponent<IAnimate>();
        }

        private void Init()
        {
        }

        public void Serve(FoodItemData item)
        {
            _animate.PlayAnimation();
        }


        public void Serve(IPutable<FoodItemData> item)
        {
            item.Put(foodItemData);
        }

        public void Put(FoodItemData item)
        {
            if (item != allowedFoodItemData) return;
            foodItemData = item;
        }

        public void OnTouch() => Serve(foodItemData);
    }
}