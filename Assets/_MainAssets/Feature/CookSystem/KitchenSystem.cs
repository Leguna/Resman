using System;
using System.Collections.Generic;
using CookSystem.Ingredient;
using UnityEngine;
using Utilities;

namespace CookSystem
{
    public class KitchenSystem : SingletonMonoBehaviour<KitchenSystem>
    {
        [SerializeField] private List<FoodPlate> foodPlates;
        [SerializeField] private List<Utensil> utensils;
        [SerializeField] private List<IngredientSource> ingredientSources;
        [SerializeField] private List<ToppingSource> toppingSources;

        public void Init(Action<FoodPlate> onPlateServe = null)
        {
            HideIngredientSources();
            foodPlates.ForEach(foodPlate => { foodPlate.Init(onPlateServe); });
            utensils.ForEach(utensil => { utensil.Init(OnUtensilServe); });
        }

        public void ShowIngredientSources()
        {
            ingredientSources.ForEach(ingredientSource => { ingredientSource.Show(); });
            toppingSources.ForEach(toppingSource => { toppingSource.Show(); });
        }

        public void HideIngredientSources()
        {
            ingredientSources.ForEach(ingredientSource => { ingredientSource.Hide(); });
            toppingSources.ForEach(toppingSource => { toppingSource.Hide(); });
        }

        private void OnUtensilServe(FoodItemData foodItemData, Utensil utensil)
        {
            var canReceive = false;

            foreach (var foodPlate in foodPlates)
            {
                var isReceived = foodPlate.TryReceive(foodItemData);
                if (isReceived)
                {
                    canReceive = true;
                    break;
                }
            }

            if (!canReceive) return;
            utensil.RemoveIngredient();
        }
    }
}