using System;
using Core;
using UnityEngine;

namespace CookSystem
{
    public class IngredientData : MyScriptableObject
    {
        public string ingredientName;
        public float currentCookTime;
        public float doneCookTime;
        public float burnTime;
        public IngredientState state;

        public Color bgColor;
        public Texture2D icon;
        public Texture2D cookedIcon;
        public Texture2D burntIcon;

        protected override void OnValidate()
        {
            base.OnValidate();
            if (doneCookTime <= 0)
                throw new Exception("Done Cook Time must be greater than 0");
            if (burnTime <= 0)
                throw new Exception("Burn Time must be greater than 0");
            if (doneCookTime >= burnTime)
                throw new Exception("Done Cook Time must be less than Burn Time");
        }

        public void ResetCookTime()
        {
            currentCookTime = 0;
        }

        public void UpdateCookTime(float amount)
        {
            currentCookTime += amount;

            state = currentCookTime switch
            {
                _ when currentCookTime < doneCookTime => IngredientState.Raw,
                _ when currentCookTime >= doneCookTime && currentCookTime < burnTime => IngredientState.Done,
                _ => IngredientState.Burnt
            };
        }

        public float GetProgress()
        {
            return state switch
            {
                IngredientState.Raw => currentCookTime / doneCookTime,
                IngredientState.Done => (currentCookTime - doneCookTime) / (burnTime - doneCookTime),
                _ => 0
            };
        }
    }

    public enum IngredientState
    {
        Raw,
        Done,
        Burnt
    }
}