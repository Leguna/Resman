using System;
using UnityEngine;

namespace CookSystem.Ingredient
{
    public class IngredientComponent : MonoBehaviour, IPlateable
    {
        [SerializeField] private IngredientData ingredient;

        public float currentCookTime;

        public IngredientState state;
        public Action onCooked;
        public Action onBurnt;
        public Action onPut;

        public void Init(IngredientData data)
        {
            ingredient = data;
        }

        public void Cook(float cookTime)
        {
            currentCookTime += cookTime;
            if (!SetStateChange()) return;
            switch (state)
            {
                case IngredientState.Done:
                    onCooked?.Invoke();
                    break;
                case IngredientState.Burnt:
                    onBurnt?.Invoke();
                    break;
            }
        }

        public void Reset()
        {
            currentCookTime = 0;
            state = IngredientState.Raw;
        }

        public Sprite GetIcon()
        {
            return state switch
            {
                IngredientState.Raw => ingredient.rawIcon,
                IngredientState.Done => ingredient.cookedIcon,
                IngredientState.Burnt => ingredient.burntIcon,
                _ => null
            };
        }

        private bool SetStateChange()
        {
            var lastState = state;
            var newState = currentCookTime switch
            {
                _ when currentCookTime < ingredient.doneCookTime => IngredientState.Raw,
                _ when currentCookTime >= ingredient.doneCookTime && currentCookTime < ingredient.burnTime =>
                    IngredientState.Done,
                _ => IngredientState.Burnt
            };
            if (lastState == newState) return false;
            state = newState;
            return true;
        }

        public float GetProgress()
        {
            return state switch
            {
                IngredientState.Raw => currentCookTime / ingredient.doneCookTime,
                IngredientState.Done => (currentCookTime - ingredient.doneCookTime) /
                                        (ingredient.burnTime - ingredient.doneCookTime),
                _ => 0
            };
        }

        public void OnPut()
        {
            onPut?.Invoke();
            Debug.Log("Ingredient put on plate");
        }
    }

    public interface IPlateable
    {
        void OnPut();
    }

    public enum IngredientState
    {
        Raw,
        Done,
        Burnt
    }
}