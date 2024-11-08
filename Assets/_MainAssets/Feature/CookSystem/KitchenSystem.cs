using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Init(Action<FoodItemData, FoodPlate> onPlateServe = null)
        {
            HideIngredientSources();
            foodPlates.ForEach(foodPlate => { foodPlate.Init(onPlateServe); });
            utensils.ForEach(utensil => { utensil.Init(OnUtensilServe); });
        }

        public void ShowIngredientSources()
        {
            ingredientSources.ForEach(ingredientSource => { ingredientSource.Show(); });
        }

        public void HideIngredientSources()
        {
            ingredientSources.ForEach(ingredientSource => { ingredientSource.Hide(); });
        }

        private void OnUtensilServe(FoodItemData foodItemData, Utensil utensil)
        {
            if (!foodPlates.Any(foodPlate => foodPlate.TryReceive(foodItemData))) return;
            utensil.RemoveIngredient();
        }
    }
}