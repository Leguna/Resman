using System;
using UnityEngine;

namespace CookSystem.Ingredient
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "Cooking/Ingredient")]
    public class IngredientData : MyScriptableObject
    {
        public string ingredientName;
        public float doneCookTime;
        public float burnTime;

        public Sprite rawIcon;
        public Sprite cookedIcon;
        public Sprite burntIcon;

        public FoodItemData cookedFood;

        protected override void OnValidate()
        {
            try
            {
                base.OnValidate();
                if (ingredientName == null)
                    throw new Exception($"Ingredient Name cannot be null");
                if (doneCookTime <= 0)
                    throw new Exception($"Done Cook Time must be greater than 0");
                if (burnTime <= 0)
                    throw new Exception($"Burn Time must be greater than 0");
                if (doneCookTime >= burnTime)
                    throw new Exception($"Done Cook Time must be less than Burn Time");
            }
            catch (Exception e)
            {
                Debug.LogError($"Validation Error in {name}: {e.Message}", this);
            }
        }
    }

}